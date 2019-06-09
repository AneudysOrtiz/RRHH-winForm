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
    public partial class SalarioEmpleadosForm : Form
    {
        public SalarioEmpleadosForm(List<Empleado> empleados, decimal desde, decimal hasta)
        {
            InitializeComponent();

            var path = (string)crystalReportViewer1.ReportSource;
            var objRpt = new ReportDocument();
            objRpt.Load(path);

            var ds = new DataSet();
            DataTable t = ds.Tables.Add("Empleados");
            t.Columns.Add("EmpleadoId", Type.GetType("System.Int32"));
            t.Columns.Add("Cedula", Type.GetType("System.String"));
            t.Columns.Add("Nombre", Type.GetType("System.String"));
            t.Columns.Add("FechaIngreso", Type.GetType("System.DateTime"));
            t.Columns.Add("Departamento", Type.GetType("System.String"));
            t.Columns.Add("Puesto", Type.GetType("System.String"));
            t.Columns.Add("Salario", Type.GetType("System.String"));
            t.Columns.Add("Estado", Type.GetType("System.Boolean"));

            DataRow r;
            int i = 0;
            for (i = 0; i < empleados.Count; i++)
            {
                r = t.NewRow();
                r["EmpleadoId"] = empleados[i].EmpleadoId;
                r["Cedula"] = empleados[i].Cedula;
                r["Nombre"] = empleados[i].Nombre;
                r["FechaIngreso"] = empleados[i].FechaIngreso;
                r["Departamento"] = empleados[i].Departamento;
                r["Puesto"] = empleados[i].Puesto;
                r["Salario"] = empleados[i].Salario.ToString();
                r["Estado"] = empleados[i].Estado;
                t.Rows.Add(r);
            }

            objRpt.SetDataSource(ds.Tables[0]);

            ParameterFields myParams = new ParameterFields();
            var myParam = new ParameterField();
            myParam.ParameterFieldName = "SalarioDesde";
            var myDiscreteValue = new ParameterDiscreteValue();
            //myDiscreteValue.Value = string.Format("{0:C}", desde);
            myDiscreteValue.Value = desde;
            myParam.CurrentValues.Add(myDiscreteValue);
            myParams.Add(myParam);

            var myParam2 = new ParameterField();
            myParam2.ParameterFieldName = "SalarioHasta";
            var myDiscreteValue2 = new ParameterDiscreteValue();
            //myDiscreteValue2.Value = string.Format("{0:C}", hasta);
            myDiscreteValue2.Value = hasta;
            myParam2.CurrentValues.Add(myDiscreteValue2);
            myParams.Add(myParam2);

            // Assign the params collection to the report viewer
            crystalReportViewer1.ParameterFieldInfo = myParams;

            crystalReportViewer1.ReportSource = objRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
