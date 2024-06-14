using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAI
{
    public class VehicleForm : Form
    {
        public Vehicle Vehicle { get; private set; }

        private TextBox brandTextBox;
        private TextBox colorTextBox;
        private TextBox factoryNumberTextBox;
        private TextBox plateNumberTextBox;
        private DateTimePicker manufactureDatePicker;
        private TextBox constructionFeaturesTextBox;
        private TextBox paintingTextBox;
        private DateTimePicker lastInspectionDatePicker;
        private TextBox ownerPassportTextBox;

        private Button okButton;
        private Button cancelButton;



        public VehicleForm()
        {
            InitializeComponents();
            this.CenterToScreen();
        }

        private void InitializeComponents()
        {
            this.Text = "Додавання транспортного засобу";
            this.Size = new Size(500, 365);

            int labelTop = 10;
            int textBoxTop = 10;

            AddLabelAndTextBox("Марка:", ref labelTop, ref textBoxTop, ref brandTextBox);
            AddLabelAndTextBox("Колір:", ref labelTop, ref textBoxTop, ref colorTextBox);
            AddLabelAndTextBox("Заводський номер:", ref labelTop, ref textBoxTop, ref factoryNumberTextBox);
            AddLabelAndTextBox("Бортовий номер:", ref labelTop, ref textBoxTop, ref plateNumberTextBox);
            AddLabelAndDatePicker("Дата випуску:", ref labelTop, ref manufactureDatePicker);
            AddLabelAndTextBox("Особливості конструкції:", ref labelTop, ref textBoxTop, ref constructionFeaturesTextBox);
            AddLabelAndTextBox("Особливості забарвлення:", ref labelTop, ref textBoxTop, ref paintingTextBox);
            AddLabelAndDatePicker("Дата останнього тех. огляду:", ref labelTop, ref lastInspectionDatePicker);
            AddLabelAndTextBox("Паспорт власника:", ref labelTop, ref textBoxTop, ref ownerPassportTextBox);

            okButton = new Button();
            okButton.Text = "Додати";
            okButton.Location = new Point(10, textBoxTop + 280);
            okButton.DialogResult = DialogResult.OK;
            this.Controls.Add(okButton);

            cancelButton = new Button();
            cancelButton.Text = "Скасувати";
            cancelButton.Location = new Point(90, textBoxTop + 280);
            cancelButton.DialogResult = DialogResult.Cancel;
            this.Controls.Add(cancelButton);
        }

        private void AddLabelAndTextBox(string labelText, ref int labelTop, ref int textBoxTop, ref TextBox textBox)
        {
            Label label = new Label();
            label.Text = labelText;
            label.AutoSize = false;
            label.Width = 180;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Location = new Point(10, labelTop);
            this.Controls.Add(label);

            textBox = new TextBox();
            textBox.Location = new Point(200, labelTop);
            textBox.Size = new Size(250, 20);
            this.Controls.Add(textBox);

            labelTop += 30;
        }

        public static void AddVehicle()
        {
            using (var vehicleForm = new VehicleForm())
            {
                if (vehicleForm.ShowDialog() == DialogResult.OK)
                {
                    if (XmlDataManager.ContainsDigits(vehicleForm.Vehicle.Brand))
                    {
                        MessageBox.Show("Назва машини не може містити цифри.", "Помилка");
                    }
                    else if (vehicleForm.Vehicle.ManufactureDate > DateTime.Now || vehicleForm.Vehicle.LastInspectionDate > DateTime.Now)
                    {
                        MessageBox.Show("Дата випуску або останнього тех. огляду не може бути в майбутньому.", "Помилка");
                    }
                    else if (XmlDataManager.IsPlateNumberExists(vehicleForm.Vehicle.PlateNumber))
                    {
                        MessageBox.Show("Машину з таким бортовим номером вже існує.", "Помилка");
                    }
                    else if (!XmlDataManager.IsValidPassportFormat(vehicleForm.Vehicle.OwnerPassport))
                    {
                        MessageBox.Show("Паспорт власника повинен містити 9 цифр.", "Помилка");
                    }
                    else
                    {
                        XmlDataManager.WriteVehicleToXml(vehicleForm.Vehicle);
                    }
                }
            }
        }



        private void AddLabelAndDatePicker(string labelText, ref int labelTop, ref DateTimePicker datePicker)
        {
            Label label = new Label();
            label.Text = labelText;
            label.AutoSize = false;
            label.Width = 180;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Location = new Point(10, labelTop);
            this.Controls.Add(label);

            datePicker = new DateTimePicker();
            datePicker.Location = new Point(200, labelTop);
            datePicker.Size = new Size(250, 20);
            this.Controls.Add(datePicker);

            labelTop += 30;
        }

        private void InitializeComponent()
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (this.DialogResult == DialogResult.OK)
            {
                Vehicle = new Vehicle(
                    brandTextBox.Text,
                    colorTextBox.Text,
                    factoryNumberTextBox.Text,
                    plateNumberTextBox.Text,
                    manufactureDatePicker.Value,
                    constructionFeaturesTextBox.Text,
                    paintingTextBox.Text,
                    lastInspectionDatePicker.Value,
                    ownerPassportTextBox.Text);
            }
        }
    }
}
