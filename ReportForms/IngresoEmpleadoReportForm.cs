using CrystalDecisions.CrystalReports.Engine;
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
    public partial class IngresoEmpleadoReportForm : Form
    {
        public IngresoEmpleadoReportForm(List<Empleado> empleados)
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
            crystalReportViewer1.ReportSource = objRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
