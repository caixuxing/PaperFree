using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaperFree.Client.Utils.DevValidate
{
    /// <summary>
    /// 控件规则类
    /// </summary>
    public class ControlRule
    {
        public Control control;
        public ValidationRule rule;

        public ControlRule(Control control, ValidationRule rule)
        {
            this.control = control;
            this.rule = rule;
        }

        /// <summary>
        /// 判断控件是否为空
        /// </summary>
        /// <returns></returns>
        public static ConditionValidationRule NotEmpty()
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.IsNotBlank;
            rule.ErrorText = "该值不允许为空！";
            return rule;
        }

        /// <summary>
        /// 判断是否大于等于某个数
        /// </summary>
        /// <param name="number">数值</param>
        public static ConditionValidationRule NoLessNumber(decimal number)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.GreaterOrEqual;
            rule.ErrorText = string.Format("该值不能小于{0}！", number);
            rule.Value1 = number;
            return rule;
        }

        /// <summary>
        /// 判断是否大于某个数
        /// </summary>
        /// <param name="number">数值</param>
        public static ConditionValidationRule GreaterNumber(decimal number)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.Greater;
            rule.ErrorText = string.Format("该值必须大于{0}！", number);
            rule.Value1 = number;
            return rule;
        }

        /// <summary>
        /// 判断是否在指定数（包含）之间
        /// </summary>
        /// <param name="small">小数</param>
        /// <param name="big">大数</param>
        /// <returns></returns>
        public static ConditionValidationRule BetweenNumbers(decimal small, decimal big)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.Between;
            rule.ErrorText = string.Format("该值不能小于{0}且不能大于{1}！", small, big);
            rule.Value1 = small;
            rule.Value2 = big;
            return rule;
        }

        /// <summary>
        /// 判断结束时间是否不小于指定时间
        /// </summary>
        /// <param name="startTime">指定时间</param>
        public static ConditionValidationRule NoLessDate(DateTime startTime)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ErrorText = string.Format("结束时间不能小于开始时间！");
            rule.ConditionOperator = ConditionOperator.GreaterOrEqual;
            rule.Value1 = startTime;
            return rule;
        }

        /// <summary>
        /// 调用正则表达式判断输入格式
        /// </summary>
        /// <param name="strRex">正则表达式</param>
        /// <param name="isnotblank">是否允许为空</param>
        public static CustomRuleByRegex AddValueRex(string strRex, bool isnotblank,string errorMsg)
        {
            CustomRuleByRegex rule = new CustomRuleByRegex(strRex, isnotblank, errorMsg);
            return rule;
        }
    }

    
}
