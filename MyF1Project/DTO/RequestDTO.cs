using MyF1Project.Attributes;
using MyF1Project.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyF1Project.DTO
{
    public class RequestDTO
    {
        [DefaultValue(0)]
        public int PageIndex { get; set; } = 0;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;

        [DefaultValue("Id")]
        [SortColumnValidator(typeof(Race))]
        public string? SortColumn { get; set; } = "Id";

        [DefaultValue("ASC")]
        [SortOrderValidator]
        public string? SortOrder { get; set; } = "ASC";

        [DefaultValue(null)]
        public string? FilterQuery { get; set; } = null;
    }
}
