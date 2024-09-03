using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PaperFree.Client.Utils.DevValidate
{
    /// <summary>
    /// 自定义规则类
    /// </summary>
    public class CustomRuleByRegex : ValidationRule
    {
        private string regex;
        private bool isnotblank;//是否为空

        private string errorMsg;

        /// <summary>
        /// 是否为数字
        /// </summary>
        public static string strIsNumeric = "^[\\+\\-]?[0-9]*\\.?[0-9]+$";

        /// <summary>
        /// 电话号码
        /// </summary>
        public static string strPhone = @"(^(\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$|(1(([35][0-9])|(47)|[8][01236789]))\d{8}$";

        /// <summary>
        /// 使用正则表达式验证
        /// </summary>
        /// <param name="regex">正则表达式</param>
        /// <param name="isnotblank">是否允许为空</param>
        public CustomRuleByRegex(string regex, bool isnotblank,string errorMsg)
        {
            this.regex = regex;
            this.isnotblank = isnotblank;
            this.errorMsg = errorMsg;
        }

        public override bool Validate(Control control, object value)
        {
            bool flag;
            try
            {
                if (value == null || value.ToString().Trim() == string.Empty)
                {
                    if (isnotblank)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        this.ErrorText = errorMsg??"该值不允许为空！";
                    }
                }
                else
                {
                    flag = Regex.IsMatch((string)value, regex);
                    this.ErrorText = errorMsg??"输入格式不正确，请重新输入！";
                }
            }
            catch (Exception ex)
            {
                this.ErrorText = ex.Message;
                flag = false;
            }
            return flag;
        }
    }

}