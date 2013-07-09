
using CoolApp.Enums;

namespace CoolApp.Models
{
    public class ViewInfoModel
    {

        public virtual ViewInfoStatus Status { get; set; }

        public virtual string Message { get; set; }

        public virtual object Errors { get; set; }

        public virtual object Data { get; set; }

        public virtual string Redirect { get; set; }

    }
}
