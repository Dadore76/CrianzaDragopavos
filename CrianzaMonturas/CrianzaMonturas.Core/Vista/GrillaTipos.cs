﻿using CrianzaMonturas.Dal.Modelo;
using System.Data;

namespace CrianzaMonturas.Core.Vista
{
    public partial class GrillaTipos : Form
    {
        readonly DataTable sourceData;
        public DataGridViewCellCollection ValueSelected { get; set; }

        public GrillaTipos(List<Tipo> tipos)
        {
            InitializeComponent();
            sourceData = CrearDataTable(tipos);
            dgvTipos.DataSource = sourceData;
            dgvTipos.Columns[0].Visible = false;
            dgvTipos.Columns[2].Visible = false;
            dgvTipos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ValueSelected = dgvTipos.Rows[0].Cells;
        }

        private static DataTable CrearDataTable(List<Tipo> tipos)
        {
            DataTable dt = new();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Imagen", typeof(byte[]));

            foreach (Tipo tipo in tipos)
            {
                if (tipo.Id != 0)
                {
                    dt.Rows.Add(tipo.Id, tipo.Nombre, tipo.Imagen);
                }
                else
                {
                    dt.Rows.Add(tipo.Id, "", null);
                }
            }

            return dt;
        }

        private void GrillaTipos_Load(object sender, EventArgs e)
        {
            txtFiltroTipos.Focus();
        }

        private void TxtFiltroTipos_TextChanged(object sender, EventArgs e)
        {
            DataView dv = sourceData.DefaultView;
            dv.RowFilter = string.Format("Nombre Like '%{0}%'", txtFiltroTipos.Text);
        }

        private void DgvTipos_DoubleClick(object sender, EventArgs e)
        {
            int selectedRowCount = dgvTipos.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount == 1)
            {
                DataGridViewCellCollection dgdTipo = dgvTipos.SelectedRows[0].Cells;
                this.ValueSelected = dgdTipo;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DgvTipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                int selectedRowCount = dgvTipos.Rows.GetRowCount(DataGridViewElementStates.Selected);

                if (selectedRowCount == 1)
                {
                    DataGridViewCellCollection dgdTipo = dgvTipos.SelectedRows[0].Cells;
                    this.ValueSelected = dgdTipo;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void TxtFiltroTipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                int selectedRowCount = dgvTipos.Rows.GetRowCount(DataGridViewElementStates.Selected);

                if (selectedRowCount == 1)
                {
                    DataGridViewCellCollection dgdTipo = dgvTipos.SelectedRows[0].Cells;
                    this.ValueSelected = dgdTipo;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;

                int row = dgvTipos.SelectedCells[0].RowIndex;

                if (row < (dgvTipos.RowCount - 1))
                {
                    dgvTipos.ClearSelection();
                    dgvTipos.Rows[row + 1].Selected = true;
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;

                int row = dgvTipos.SelectedCells[0].RowIndex;

                if (row > 0)
                {
                    dgvTipos.ClearSelection();
                    dgvTipos.Rows[row - 1].Selected = true;
                }
            }
        }
    }
}
