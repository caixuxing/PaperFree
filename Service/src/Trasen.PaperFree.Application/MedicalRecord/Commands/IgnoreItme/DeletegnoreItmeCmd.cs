using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.IgnoreItme;
/// <summary>
/// 删除忽略目录
/// </summary>
/// <param name="id"></param>
public record DeletegnoreItmeCmd(string id) : IRequest<bool>;