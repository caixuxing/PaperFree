##  项目文档
--------------------
### 1.项目部署基础信息

>1.[堡垒机](http://wtx.kinghis.xyz/core/auth/login/  )

>2.[jenkins](http://192.168.18.86:10240) <br/>
账号：jenkins   
密码：wtx@123

>3.[Portainer](http://192.168.18.86:9000/) <br/>
账号：admin <br/>
密码：1234567890

>4.[swaggerApi接口文档](http://192.168.18.86:8065/swagger/index.html) <br/>

>5.[前端Web](http://192.168.18.86:8064) <br/>
账号：wzhba <br/>
密码：1



>5.[Minio文件对象存储](http://192.168.18.86:9002/) <br/>
账号：admin <br/>
密码：admin123

```
==================redis=====================
Host:192.168.18.86   port:6379   密码：wtx123
    
```

---------------
### 2.项目结构说明
![](Image/服务端项目结构.png)

--------------------
### 3.开发工具准备
```
1.Vs2022 Net6.0

2.Oracle数据库 、Redis

3.前端Vue

```
--------------------
### 4.技术知识储备

>[EF Core官网文档](https://learn.microsoft.com/zh-cn/ef/core/)

>[FluentValidation 案例文档](https://docs.fluentvalidation.net/en/latest/index.html) <br/>
[参考使用案例](https://www.cnblogs.com/xwc1996/p/13956031.html)

>[FluentAPI详细用法](https://blog.csdn.net/WuLex/article/details/111976068)
```
DDD CQRS、Net Core 、EF Core、 ORM 、FluentAPI、SQL、Hangfire、FluentValidation、MediatR、IOC

```


--------------------
### 5.系统迁移

>[迁移参考文档](https://learn.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/managing?tabs=vs)
```
1.Add-Migration -Context AppDbContext <自定义文件名称>            #生成迁移脚本文件
 
2.Update-Database -Context AppDbContext                           #脚本更新到数据库（如果回退本版 格式：Update-Database -Context AppDbContext <自定义文件名称>）

3.Script-Migration -Context AppDbContext                         #生成SQL脚本

```




### 6.Markdown 
>[Markdown基础语法教程](https://markdown.com.cn)