#region License, Terms and Author(s)

//
// ELMAH - Error Logging Modules and Handlers for ASP.NET
// Copyright (c) 2004-9 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Atif Aziz, http://www.raboof.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

#region

using System;
using System.Web.UI;
using Elmah;

#endregion

[assembly: Scc("$Id: ErrorLoginPage.cs addb64b2f0fa 2012-08-25 18:50:16Z ockertvanheerden $")]

namespace Elmah
{

    /// <summary>
    /// Renders an HTML page displaying the login page
    /// error log.
    /// </summary>
    internal sealed class ErrorLoginPage : ErrorPageBase
    {
        //private bool IsLogedIn;

        protected override void OnLoad(EventArgs e)
        {
            this.PageTitle = string.Format("Login Required");

            if (!SecurityConfiguration.Default.RequireLogin)
            {
                Response.Redirect(Request.Url.AbsolutePath.Remove(Request.Url.AbsolutePath.LastIndexOf("/login", StringComparison.CurrentCultureIgnoreCase)), true);
                return;
            }

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                if (Request.UrlReferrer == Request.Url && !string.IsNullOrEmpty(Request["username"]) && !string.IsNullOrEmpty(Request["password"]))
                {
                    if (Request["username"].Equals(SecurityConfiguration.Default.UserName, StringComparison.CurrentCultureIgnoreCase) && Request["password"].Equals(SecurityConfiguration.Default.Password))
                    {
                        HttpRequestSecurity.LogIn(Request["username"], Context);
                        Response.Redirect(Request.AppRelativeCurrentExecutionFilePath, true);
                        return;
                    }
                }
            }

            Response.Status = "403 Forbidden";

            base.OnLoad(e);
        }


       

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            RenderError(writer);
        }

        private void RenderError(HtmlTextWriter writer)
        {
            Debug.Assert(writer != null);

            writer.AddAttribute("action", Request.Url.ToString());
            writer.AddAttribute("Method", "POST");
            writer.RenderBeginTag(HtmlTextWriterTag.Form);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "login");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //heading
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "login-table");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); //row1
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.RenderBeginTag(HtmlTextWriterTag.H1);
            writer.Write("Please Log in");
            writer.RenderEndTag();
            writer.RenderEndTag(); //td
            writer.RenderEndTag(); //row1

            //Username label
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); //row2
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "login-label");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("UserName:");
            writer.RenderEndTag();
            writer.RenderEndTag(); //td
            writer.RenderEndTag(); //row2

            //username textbox
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); //row3
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "username");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "username");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag(); //td
            writer.RenderEndTag(); //row3

            //password label
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); //row4
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "login-label");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("Password:");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.RenderEndTag(); //td
            writer.RenderEndTag(); //row4

            //password textbox
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); //row5
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "password");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "password");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "password");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag(); //td
            writer.RenderEndTag(); //row5

            //Submit button
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); //row6
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "Submit");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag(); //td
            writer.RenderEndTag(); //row6
            writer.RenderEndTag(); //table

            writer.RenderEndTag(); //div
            writer.RenderEndTag(); //form
        }

        protected override void RenderDocumentEnd(HtmlTextWriter writer)
        {
            writer.RenderEndTag(); // </body>
            writer.WriteLine();

            writer.RenderEndTag(); // </html>
            writer.WriteLine();
        }
    }
}