//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ded_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class Date
    {
        public int ID_Date { get; set; }
        public Nullable<int> ID_Number { get; set; }
        public Nullable<int> ID_Profile { get; set; }
        public System.DateTime dateFrom { get; set; }
        public System.DateTime dateTo { get; set; }
        public string order_state { get; set; }
    
        public virtual Number Number { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual State State { get; set; }
    }
}
