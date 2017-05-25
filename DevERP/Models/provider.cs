using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevERP.Models
{
    public class Provider
    {
        public static string GetSuccessMassageFormated(string msg)
        {
            return "<div class='alert alert-success alert-dismissible' role='alert'>" +
                                     "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                                     "<span aria-hidden='true'>&times;</span></button>" + msg + "</b></div>";
        }
        public static string GetErrorMassageFormated(string msg)
        {
            return "<div class='alert alert-danger alert-dismissible' role='alert'>" +
                                     "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                                     "<span aria-hidden='true'>&times;</span></button>" + msg + "</b></div>";
        }
    }
}