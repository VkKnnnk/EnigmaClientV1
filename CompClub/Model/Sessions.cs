//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompClub.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sessions
    {
        public int sessionId { get; set; }
        public int userId { get; set; }
        public int tariffId { get; set; }
        public System.DateTime start_session { get; set; }
        public System.DateTime end_session { get; set; }
        public int computerId { get; set; }
        public bool status { get; set; }
    
        public virtual Computers Computers { get; set; }
        public virtual Tarrifs Tarrifs { get; set; }
        public virtual Users Users { get; set; }
    }
}
