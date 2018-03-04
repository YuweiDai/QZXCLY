using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QZCHY.Web.Framework.Filters
{
    public class UrlDecodeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            var task = actionExecutedContext.Response.Content.ReadAsStringAsync();

            HttpContent content = new StringContent(HttpUtility.UrlEncode(task.Result));

            actionExecutedContext.Response.Content = content;
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Dictionary<string, object> Param = actionContext.ActionArguments;
            Dictionary<string, string> ParamTemp = new Dictionary<string, string>();
            foreach (string item in Param.Keys)
            {
                object Value = Param[item];
                if (Value == null)
                    continue;
                string StrValue = Value.ToString();
                if (string.IsNullOrEmpty(StrValue))
                    continue;
                ParamTemp.Add(item, HttpUtility.UrlDecode(StrValue).Replace("??", ""));
            }
            foreach (var item in ParamTemp.Keys)
            {
                actionContext.ActionArguments[item] = ParamTemp[item];
            }
            base.OnActionExecuting(actionContext);
        }
    }

}
