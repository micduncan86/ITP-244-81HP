﻿using System;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //setting jQery validation to false
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
        {
            //only on initial page load, setting input controls to default values.
            numSalePrice.Text = "0";
            numDiscountPercentage.Text = "0";
            lblCalcDiscountAmt.Text = "";
            lblCalcTotalAmt.Text = "";
            Session.Timeout = 2;
            Session.Clear();
           
        }

    }
    protected void Calculate(object sender, EventArgs e)
    {
        // establish local variables for hold for calculations.
        Decimal _SalesPriceAmt = 0;
        Decimal _DiscountPercentage = 0;
        
        //Conditional check to see if Sales Price and Discount Percentage are Numerical Values
        if (Decimal.TryParse(numSalePrice.Text,out _SalesPriceAmt)
            && Decimal.TryParse(numDiscountPercentage.Text, out _DiscountPercentage))
        {
            Decimal _DiscountAmt = _SalesPriceAmt * (_DiscountPercentage * (Decimal)0.01);
            //Utilizing built in string formatting for currency to set label text.
            lblCalcDiscountAmt.Text = String.Format("{0:C}",_DiscountAmt);
            lblCalcTotalAmt.Text = String.Format("{0:C}", _SalesPriceAmt - _DiscountAmt);
            Session.Add("Sale_Price", _SalesPriceAmt);
            Session.Add("Discount_Amount", _DiscountAmt);
            Session.Add("Total_Price", _SalesPriceAmt - _DiscountAmt);
            return;
        }
        lblCalcDiscountAmt.Text = "Could not calculate.";
        lblCalcTotalAmt.Text = "Could not calculate.";
    }
    protected void Confirm(object sender, EventArgs e)
    {
         if (Session.Keys.Count != 0)
        {
            Response.Redirect("Confirm.aspx");
        }

         //dynamically create label when necessary for when the user has not calculated discount.
        System.Web.UI.WebControls.Label lblConfirmError = new System.Web.UI.WebControls.Label();
        lblConfirmError.ID = "lblConfirmError";
        lblConfirmError.Text = "<br />You must calculate discount before confirming.";
        lblConfirmError.CssClass = "validator";
        form1.Controls.Add(lblConfirmError);
        
    }

}