using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RockPaperScissors
{
    public partial class About : Page
    {
        public DataTable winners = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

                SQLConector conector = new SQLConector();

                conector.openConection();
                winners = conector.selectWinners();
                conector.closeConection();

                gridWinners.DataSource = winners;
                gridWinners.DataBind();
        }

        protected void reset_click(object sender, EventArgs e)
        {
            SQLConector conector = new SQLConector();

            conector.deletetWinners();

            conector.openConection();
            winners = conector.selectWinners();
            conector.closeConection();

            gridWinners.DataSource = winners;
            gridWinners.DataBind();
        }
    }
}