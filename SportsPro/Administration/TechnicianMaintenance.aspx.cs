using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Administration
{
    public partial class TechnicianMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTechnicians();
            }
        }
        protected void LoadTechnicians(int TechID = -1, FormViewMode _mode = FormViewMode.ReadOnly)
        {
            if (Session["TechnicianList"] == null)
            {
                SportsProLibrary.Technicians Technicians = new SportsProLibrary.Technicians();
                Session["TechnicianList"] = Technicians.GetTechnicians();
            }

            ddlTechnicians.DataSource = (List<SportsProLibrary.oTechnician>)Session["TechnicianList"];
            ddlTechnicians.DataTextField = "Name";
            ddlTechnicians.DataValueField = "TechID";
            ddlTechnicians.DataBind();
            if (TechID != -1)
            {
                ddlTechnicians.SelectedValue = TechID.ToString();
            }
            else
            {
                TechID = Convert.ToInt32(ddlTechnicians.SelectedValue);
            }

            LoadTechForm(_mode, TechID);
        }

        protected void LoadTechForm(List<SportsProLibrary.oTechnician> _Tech = null)
        {
            if (_Tech == null)
            {
                _Tech = ((List<SportsProLibrary.oTechnician>)Session["TechnicianList"]).Where(p => p.TechID == Convert.ToInt32(ddlTechnicians.SelectedValue)).ToList();
            }
            frmTechnician.DataSource = _Tech;
            frmTechnician.DataBind();
        }
        protected void LoadTechForm(FormViewMode _mode, int _techID)
        {
            frmTechnician.ChangeMode(_mode);
            List<SportsProLibrary.oTechnician> _techs = ((List<SportsProLibrary.oTechnician>)Session["TechnicianList"]).Where(p => p.TechID == _techID).ToList();
            LoadTechForm(_techs);
        }

        protected void ddlTechnicians_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTechForm();
        }
        protected void DeleteTech(int _techID)
        {
            SportsProLibrary.oTechnician _tech = ((List<SportsProLibrary.oTechnician>)Session["TechnicianList"]).FirstOrDefault(p => p.TechID == _techID);
            _tech.Delete();
            lblError.Text = _tech.Name + " has been removed.";
            ((List<SportsProLibrary.oTechnician>)Session["TechnicianList"]).Remove(_tech);
            LoadTechnicians();
        }
        protected void UpdateTech(int _techID)
        {
            SportsProLibrary.oTechnician _tech = ((List<SportsProLibrary.oTechnician>)Session["TechnicianList"]).FirstOrDefault(p => p.TechID == _techID);
            _tech.Name = ((TextBox)frmTechnician.FindControl("txtName")).Text;
            _tech.Phone = ((TextBox)frmTechnician.FindControl("txtPhone")).Text;
            _tech.Email = ((TextBox)frmTechnician.FindControl("txtEmail")).Text;
            _tech.Save();
            lblError.Text = _tech.Name + " has been updated.";
            LoadTechnicians(_techID);
        }
        protected void AddTech()
        {
            SportsProLibrary.oTechnician newTech = new SportsProLibrary.oTechnician();
            newTech.TechID = Convert.ToInt32(((TextBox)frmTechnician.FindControl("txtTechID")).Text);
            newTech.Name = ((TextBox)frmTechnician.FindControl("txtName")).Text;
            newTech.Phone = ((TextBox)frmTechnician.FindControl("txtPhone")).Text;
            newTech.Email = ((TextBox)frmTechnician.FindControl("txtEmail")).Text;
            newTech.Add();
            lblError.Text = newTech.Name + " has been added.";
            ((List<SportsProLibrary.oTechnician>)Session["TechnicianList"]).Add(newTech);
            LoadTechnicians(newTech.TechID);
        }

        protected void frmTechnician_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {
                int ID = -1;
                Int32.TryParse(e.CommandArgument.ToString(), out ID);
                switch (e.CommandName)
                {
                    case "deleteTech":
                        DeleteTech(ID);
                        break;
                    case "editTech":
                        LoadTechForm(FormViewMode.Edit, ID);
                        break;
                    case "cancelTech":
                        LoadTechForm(FormViewMode.ReadOnly, Convert.ToInt32(ddlTechnicians.SelectedValue));
                        break;
                    case "updateTech":
                        UpdateTech(ID);
                        break;
                    case "newTech":
                        LoadTechForm(FormViewMode.Insert, ID);
                        break;
                    case "addTech":
                        AddTech();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void frmTechnician_DataBound(object sender, EventArgs e)
        {
            if (frmTechnician.CurrentMode == FormViewMode.Insert)
            {
                TextBox txtID = ((TextBox)frmTechnician.FindControl("txtTechID"));
                txtID.Text = new SportsProLibrary.Technicians().GetNextUniqueID().ToString();
            }



        }
    }
}