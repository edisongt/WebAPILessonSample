//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPILessonOne.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderLine
    {
        public int OrderId { get; set; }
        public int LineNumber { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal LineTotal { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
