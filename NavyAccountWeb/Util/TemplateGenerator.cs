using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountWeb.Util
{
    public class TemplateGenerator
    {

        public static string GetHTMLString(IEnumerable<LedgersView> ledgersViews)
        {
            
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Code</th>
                                        <th>Decsription</th>
                                        <th>Credit Balance</th>
                                        <th>Debit Balance</th>
                                    </tr>");

            foreach (var ledger in ledgersViews)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", ledger.code, ledger.desc, ledger.crbal, ledger.dbbal);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
