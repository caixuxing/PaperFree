using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.RecallRecord.Entity
{
    /// <summary>
    /// 召回档案申请关联表
    /// </summary>
    public record RecallApplyAndArchival : FullRoot
    {
        public RecallApplyAndArchival(string archivalId, string recallApplyId)
        {
            ArchivalId = archivalId;
            RecallApplyId = recallApplyId;
        }

        private RecallApplyAndArchival() { }
        /// <summary>
        /// 档案号
        /// </summary>
        public string ArchivalId { get; private set; }
        /// <summary>
        /// 召回申请ID
        /// </summary>
        public string RecallApplyId { get; private set; }

        public virtual RecallApply RecallApply { get; private set; }
    }
}