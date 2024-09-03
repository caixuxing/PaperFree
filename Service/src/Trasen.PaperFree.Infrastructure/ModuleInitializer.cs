using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Hangfire.Heartbeat.Server;
using Hangfire.HttpJob;
using Hangfire.Redis.StackExchange;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Minio;
using Trasen.PaperFree.Application.Data;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.BasicData.Repository;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;
using Trasen.PaperFree.Domain.MedicalRecord.Repository;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Config;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Response;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.ArchiveRecord;
using Trasen.PaperFree.Infrastructure.Database.BasicData;
using Trasen.PaperFree.Infrastructure.Database.DbContext;
using Trasen.PaperFree.Infrastructure.Database.IgnoreItme;
using Trasen.PaperFree.Infrastructure.Database.MedicalRecord;
using Trasen.PaperFree.Infrastructure.Database.PatientDetails;
using Trasen.PaperFree.Infrastructure.Database.ProcessRecord;
using Trasen.PaperFree.Infrastructure.Database.RecallRecord;
using Trasen.PaperFree.Infrastructure.Database.SystemBasicData;
using Trasen.PaperFree.Infrastructure.HangfireTasks;
using Trasen.PaperFree.Infrastructure.SeedWork;
using Trasen.PaperFree.Infrastructure.SeedWork.Redis;
using Trasen.PaperFree.Infrastructure.SignalR;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Trasen.PaperFree.Infrastructure;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {



        services.AddTransient<IGuidGenerator, SequentialGuidGenerator>();
        services.AddTransient<ICurrentUser, CurrentUser>();
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true);
        services.AddHangfire(Hangfire);
        services.AddHangfireServer(HangfireServer);
        //services.AddHostedService<FirstStartService>();
        var minio = Appsetting.Instance.GetSection("MinIO").Get<MiniIoConfig>();
        services.AddSingleton<IMinioClient>(new MinioClient()
            .WithEndpoint(minio.Endpoint)
            .WithCredentials(minio.AccessKey, minio.SecretKey)
            .Build());
        services.AddSingleton<IMinioFileService, MinioFileService>();
        services.AddSingleton<IEmail, Email>();
        services.AddSingleton<PersonHub>();
        services.AddSignalR();
        services.AddSingleton<IRedisProvider, DefaultRedisProvider>();
        services.AddSingleton<IRedisService, RedisService>();
        services.AddScoped<ISysOperLogRepo, SysOperLogRepo>();
        services.AddScoped<IProcessDesignRepo, ProcessDesignRepo>();
        services.AddScoped<IProcessNodeRepo, ProcessNodeRepo>();
        services.AddScoped<IArchiveApplyRepo, ArchiveApplyRepo>();
        services.AddScoped<IArchiveApproverRepo, ArchiveApproverRepo>();
        services.AddScoped<IBaseBorrowModeRepo, BaseBorrowModeRepo>();
        services.AddScoped<ICaseShelfManagementRepo, CaseShelfManagementRepo>();
        services.AddScoped<IBaseWatermarkRepo, BaseWatermarkRepo>();
        services.AddScoped<IEssentialDocumentsRepo, EssentialDocumentRepo>();
        services.AddScoped<IPayConfigRepo, PayConfigRepo>();
        services.AddScoped<IAnnotationTableRepo, AnnotationTableRepo>();
        services.AddScoped<IOutpatientInfoRepo, OutpatientInfoRepo>();
        services.AddScoped<IDeptMenuTreeRepo, DeptMeunTreeRepo>();
        services.AddScoped<IArchiverMeumRepo, ArchiverMeumRepo>();
        services.AddScoped<IFilesHisRepo, FilesHisRepo>();
        services.AddTransient<IFilesHisRepo, FilesHisRepo>();
        services.AddTransient<IFilesOtherRepo, FilesOtherRepo>();
        services.AddScoped<IRecallApplyRepo, RecallApplyRepo>();
        services.AddScoped<IInpatientInfoRepo, InPatientInfoRepo>();
        services.AddScoped<IOutpatientEmergencyRepo, OutpatientEmergencyRepo>();
        services.AddScoped<IIgnoreItmeRepo, IgnoreItmeRepo>();

        #region 基础数据仓储

        services.AddScoped<IBasOrgMemberRepo, BasOrgMemberRepo>();

        #endregion 基础数据仓储
    }

    private void Hangfire(IGlobalConfiguration globalConfiguration)
    {
        var redisSetting = Appsetting.Instance.GetSection("RedisConfig").Get<RedisSetting>();

        var emailAlarmConfig = Appsetting.Instance.GetSection("EmailAlarmConfig").Get<EmailAlarmConfig>();
        globalConfiguration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)//此方法 只初次创建数据库使用即可
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseRedisStorage($"{redisSetting.Host}:{redisSetting.Port},password={redisSetting.Password}", new RedisStorageOptions()
            {
                Db = 2,
                 FetchTimeout = TimeSpan.FromSeconds(5),
                 Prefix = "hangfire:",
                SucceededListSize = 100,
                DeletedListSize = 100,
                ExpiryCheckInterval = TimeSpan.FromSeconds(2),
                InvisibilityTimeout = TimeSpan.FromSeconds(5),
                UseTransactions = true
            })
            .UseConsole(new ConsoleOptions { BackgroundColor = "#000000" })
            .UseHangfireHttpJob(new HangfireHttpJobOptions()
            {
                DashboardTitle = "Hangfire 管理",
                MailOption=new MailOption { 
                    Server= emailAlarmConfig.Host,
                    Port = emailAlarmConfig.Port,
                    User = emailAlarmConfig.From,
                    Password= emailAlarmConfig.Password,
                    UseSsl =true
                }
            }
            )
            .UseDashboardMetrics(new DashboardMetric[] { 
                DashboardMetrics.AwaitingCount,
                DashboardMetrics.ProcessingCount,
                DashboardMetrics.RecurringJobCount,
                DashboardMetrics.RetriesCount, 
                DashboardMetrics.FailedCount,
                DashboardMetrics.SucceededCount });
        GlobalStateHandlers.Handlers.Add(new SucceededStateExpireHandler(int.Parse("60")));
    }

    private void HangfireServer(BackgroundJobServerOptions options)
    {
        options.Queues = new[] { "img-queue", "log-queue", "default" }; //队列名称，只能为小写
        options.WorkerCount = Environment.ProcessorCount * 5; //并发任务数
        options.ServerName = "hangfire1Test_CCX";
        options.SchedulePollingInterval = TimeSpan.FromSeconds(15);



    }
}