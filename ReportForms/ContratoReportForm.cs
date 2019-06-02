using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using RRHHOrtiz.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHHOrtiz.ReportForms
{
    public partial class ContratoReportForm : Form
    {
        public ContratoReportForm(Candidato candidato)
        {
            InitializeComponent();

            var path = (string)crystalReportViewer1.ReportSource;
            var report = new ReportDocument();
            report.Load(path);


            // Create parameter objects
            ParameterFields myParams = new ParameterFields();
            ParameterField myParam = new ParameterField();
            ParameterDiscreteValue myDiscreteValue = new ParameterDiscreteValue();
            

            // Add first country
            myParam.ParameterFieldName = "Nombre";
            myDiscreteValue.Value = candidato.Nombre;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            myParam = new ParameterField();
            myParam.ParameterFieldName = "NombreCopia";
            myDiscreteValue = new ParameterDiscreteValue();
            myDiscreteValue.Value = candidato.Nombre;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            //// Reuse myDiscreteValue, and assign second country
            myParam = new ParameterField();
            myParam.ParameterFieldName = "Cedula";
            myDiscreteValue = new ParameterDiscreteValue();
            myDiscreteValue.Value = candidato.Cedula;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            myParam = new ParameterField();
            myParam.ParameterFieldName = "Direccion";
            myDiscreteValue = new ParameterDiscreteValue();
            myDiscreteValue.Value = candidato.Direccion;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            myParam = new ParameterField();
            myParam.ParameterFieldName = "Salario";
            myDiscreteValue = new ParameterDiscreteValue();
            myDiscreteValue.Value = candidato.Salario;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            myParam = new ParameterField();
            myParam.ParameterFieldName = "Puesto";
            myDiscreteValue = new ParameterDiscreteValue();
            myDiscreteValue.Value = candidato.Puesto.Nombre;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            myParam = new ParameterField();
            myParam.ParameterFieldName = "Departamento";
            myDiscreteValue = new ParameterDiscreteValue();
            myDiscreteValue.Value = candidato.Departamento;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            // Assign the params collection to the report viewer
            crystalReportViewer1.ParameterFieldInfo = myParams;

            // Assign the Report to the report viewer.
            // This method uses a strongly typed report,
            // but other methods are possible as well.
            crystalReportViewer1.ReportSource = report;

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}
