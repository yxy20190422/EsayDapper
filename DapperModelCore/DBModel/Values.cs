using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    [Display(Rename = "valuestest")]
    public partial class Values
    {
        private int _id;
        private string _keys;
        private string _vals;


        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = @"自增ID")]
        [Identity]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// @"键"
        /// </summary>
        [StringLength(50)]
        [Display(Name = @"键")]
        public string Keys
        {
            get
            {
                return _keys;
            }
            set
            {
                _keys = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 50)
                {
                    _keys = value.Substring(0, 50);
                }
            }
        }

        /// <summary>
        /// @"值"
        /// </summary>
        [StringLength(100)]
        [Display(Name = @"值")]
        public string Vals
        {
            get
            {
                return _vals;
            }
            set
            {
                _vals = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 100)
                {
                    _vals = value.Substring(0, 100);
                }
            }
        }

    }

}

