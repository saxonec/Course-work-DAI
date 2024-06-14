using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAI
{
    public class XmlDataManager
    {
        private string filePath;

        public XmlDataManager(string filePath)
        {
            this.filePath = filePath;
        }


        public static void WriteVehicleToXml(Vehicle vehicle)
        {
            if (ContainsDigits(vehicle.Brand))
            {
                MessageBox.Show("Назва машини не може містити цифри.", "Помилка");
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();

            string filePath = "vehicles.xml";
            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
            }
            else
            {
                XmlElement root = xmlDoc.CreateElement("Vehicles");
                xmlDoc.AppendChild(root);
            }

            XmlElement vehicleElement = xmlDoc.CreateElement("Vehicle");

            XmlAttribute brandAttribute = xmlDoc.CreateAttribute("Brand");
            brandAttribute.Value = vehicle.Brand;
            vehicleElement.Attributes.Append(brandAttribute);

            XmlAttribute colorAttribute = xmlDoc.CreateAttribute("Color");
            colorAttribute.Value = vehicle.Color;
            vehicleElement.Attributes.Append(colorAttribute);

            XmlAttribute factoryNumberAttribute = xmlDoc.CreateAttribute("FactoryNumber");
            factoryNumberAttribute.Value = vehicle.FactoryNumber;
            vehicleElement.Attributes.Append(factoryNumberAttribute);

            XmlAttribute plateNumberAttribute = xmlDoc.CreateAttribute("PlateNumber");
            plateNumberAttribute.Value = vehicle.PlateNumber;
            vehicleElement.Attributes.Append(plateNumberAttribute);

            XmlAttribute manufactureDateAttribute = xmlDoc.CreateAttribute("ManufactureDate");
            manufactureDateAttribute.Value = vehicle.ManufactureDate.ToString("yyyy-MM-dd");
            vehicleElement.Attributes.Append(manufactureDateAttribute);

            XmlAttribute constructionFeaturesAttribute = xmlDoc.CreateAttribute("ConstructionFeatures");
            constructionFeaturesAttribute.Value = vehicle.ConstructionFeatures;
            vehicleElement.Attributes.Append(constructionFeaturesAttribute);

            XmlAttribute paintingAttribute = xmlDoc.CreateAttribute("Painting");
            paintingAttribute.Value = vehicle.Painting;
            vehicleElement.Attributes.Append(paintingAttribute);

            XmlAttribute lastInspectionDateAttribute = xmlDoc.CreateAttribute("LastInspectionDate");
            lastInspectionDateAttribute.Value = vehicle.LastInspectionDate.ToString("yyyy-MM-dd");
            vehicleElement.Attributes.Append(lastInspectionDateAttribute);

            XmlAttribute ownerPassportAttribute = xmlDoc.CreateAttribute("OwnerPassport");
            ownerPassportAttribute.Value = vehicle.OwnerPassport;
            vehicleElement.Attributes.Append(ownerPassportAttribute);

            xmlDoc.DocumentElement?.AppendChild(vehicleElement);

            xmlDoc.Save(filePath);

            MessageBox.Show("Транспортний засіб доданий.", "Результат");

        }

        public static bool ContainsDigits(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPlateNumberExists(string plateNumber)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filePath = "vehicles.xml";

            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                XmlNodeList vehicleNodes = xmlDoc.SelectNodes("/Vehicles/Vehicle");

                foreach (XmlNode node in vehicleNodes)
                {
                    string existingPlateNumber = node.Attributes["PlateNumber"]?.Value;
                    if (existingPlateNumber == plateNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsValidPassportFormat(string passport)
        {
            if (passport.Length != 9)
            {
                return false;
            }

            foreach (char c in passport)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }


        public static void ShowVehicles()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filePath = "vehicles.xml";

            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                XmlNodeList vehicleNodes = xmlDoc.SelectNodes("/Vehicles/Vehicle");

                if (vehicleNodes != null && vehicleNodes.Count > 0)
                {
                    DataTable vehiclesTable = new DataTable();
                    vehiclesTable.Columns.Add("Марка");
                    vehiclesTable.Columns.Add("Колір");
                    vehiclesTable.Columns.Add("Заводський номер");
                    vehiclesTable.Columns.Add("Бортовий номер");
                    vehiclesTable.Columns.Add("Дата випуску");
                    vehiclesTable.Columns.Add("Особливості конструкції");
                    vehiclesTable.Columns.Add("Особливості забарвлення");
                    vehiclesTable.Columns.Add("Дата останнього тех. огляду");
                    vehiclesTable.Columns.Add("Паспорт власника");

                    foreach (XmlNode node in vehicleNodes)
                    {
                        string brand = node.Attributes["Brand"]?.Value;
                        string color = node.Attributes["Color"]?.Value;
                        string factoryNumber = node.Attributes["FactoryNumber"]?.Value;
                        string plateNumber = node.Attributes["PlateNumber"]?.Value;
                        string manufactureDate = node.Attributes["ManufactureDate"]?.Value;
                        string constructionFeatures = node.Attributes["ConstructionFeatures"]?.Value;
                        string painting = node.Attributes["Painting"]?.Value;
                        string lastInspectionDate = node.Attributes["LastInspectionDate"]?.Value;
                        string ownerPassport = node.Attributes["OwnerPassport"]?.Value;

                        vehiclesTable.Rows.Add(brand, color, factoryNumber, plateNumber, manufactureDate, constructionFeatures, painting, lastInspectionDate, ownerPassport);
                    }

                    DataGridView vehiclesDataGridView = new DataGridView();
                    vehiclesDataGridView.Dock = DockStyle.Fill;
                    vehiclesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    vehiclesDataGridView.DataSource = vehiclesTable;

                    Form vehiclesForm = new Form();
                    vehiclesForm.Text = "Додані транспортні засоби";
                    vehiclesForm.Size = new Size(1000, 600);
                    vehiclesForm.Controls.Add(vehiclesDataGridView);

                    vehiclesForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("У файлі немає доданих транспортних засобів.", "Повідомлення");
                }
            }
            else
            {
                MessageBox.Show("Файл не знайдено.", "Помилка");
            }

        }

        public static void RemoveVehicle()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filePath = "vehicles.xml";

            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                XmlNodeList vehicleNodes = xmlDoc.SelectNodes("/Vehicles/Vehicle");

                if (vehicleNodes != null && vehicleNodes.Count > 0)
                {
                    int formHeight = 150 + vehicleNodes.Count * 30;

                    Form removeForm = new Form();
                    removeForm.Text = "Видалення транспортного засобу";
                    removeForm.Size = new Size(400, formHeight);

                    int buttonTop = 10;
                    int index = 1;
                    foreach (XmlNode node in vehicleNodes)
                    {
                        string brand = node.Attributes["Brand"]?.Value;
                        string plateNumber = node.Attributes["PlateNumber"]?.Value;

                        RadioButton radioButton = new RadioButton();
                        radioButton.Text = $"{index}. Марка: {brand}, Бортовий номер: {plateNumber}";
                        radioButton.AutoSize = true;
                        radioButton.Location = new Point(10, buttonTop);
                        radioButton.Tag = node;
                        removeForm.Controls.Add(radioButton);

                        buttonTop += 30;
                        index++;
                    }

                    Button okButton = new Button();
                    okButton.Text = "Видалити";
                    okButton.Location = new Point(10, buttonTop);
                    okButton.DialogResult = DialogResult.OK;
                    removeForm.Controls.Add(okButton);

                    Button cancelButton = new Button();
                    cancelButton.Text = "Скасувати";
                    cancelButton.Location = new Point(90, buttonTop);
                    cancelButton.DialogResult = DialogResult.Cancel;
                    removeForm.Controls.Add(cancelButton);

                    if (removeForm.ShowDialog() == DialogResult.OK)
                    {
                        foreach (Control control in removeForm.Controls)
                        {
                            if (control is RadioButton radioButton && radioButton.Checked)
                            {
                                XmlNode nodeToRemove = (XmlNode)radioButton.Tag;
                                xmlDoc.DocumentElement?.RemoveChild(nodeToRemove);
                                xmlDoc.Save(filePath);
                                MessageBox.Show("Транспортний засіб видалений.", "Результат");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("У файлі немає доданих транспортних засобів.", "Повідомлення");
                }
            }
            else
            {
                MessageBox.Show("Файл не знайдено.", "Помилка");
            }
        }

        public static void ShowOverdueVehicles()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filePath = "vehicles.xml";

            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                XmlNodeList vehicleNodes = xmlDoc.SelectNodes("/Vehicles/Vehicle");

                if (vehicleNodes != null && vehicleNodes.Count > 0)
                {
                    string overdueVehicles = "Транспортні засоби з ост. тех. оглядом більше 365 днів тому:\n";
                    foreach (XmlNode node in vehicleNodes)
                    {
                        string platenumber = node.Attributes["PlateNumber"]?.Value;
                        string lastInspectionDate = "2022-01-01";

                        DateTime inspectionDate = DateTime.Parse(lastInspectionDate);
                        if (DateTime.Now.Subtract(inspectionDate).TotalDays > 365)
                        {
                            overdueVehicles += $"{platenumber}\n";
                        }
                    }

                    MessageBox.Show(overdueVehicles, "Запрошення на техогляд");
                }
                else
                {
                    MessageBox.Show("У файлі немає доданих транспортних засобів.", "Повідомлення");
                }
            }
            else
            {
                MessageBox.Show("Файл не знайдено.", "Помилка");
            }
        }

        public static void ShowVehiclesByTemplate()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filePath = "vehicles.xml";

            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                XmlNodeList vehicleNodes = xmlDoc.SelectNodes("/Vehicles/Vehicle");

                if (vehicleNodes != null && vehicleNodes.Count > 0)
                {
                    string template = Microsoft.VisualBasic.Interaction.InputBox("Введіть відому інформацію про транспортний засіб:", "Пошук за шаблоном");

                    if (!string.IsNullOrEmpty(template))
                    {
                        DataTable vehiclesTable = new DataTable();
                        vehiclesTable.Columns.Add("Марка");
                        vehiclesTable.Columns.Add("Колір");
                        vehiclesTable.Columns.Add("Заводський номер");
                        vehiclesTable.Columns.Add("Бортовий номер");
                        vehiclesTable.Columns.Add("Дата випуску");
                        vehiclesTable.Columns.Add("Особливості конструкції");
                        vehiclesTable.Columns.Add("Особливості забарвлення");
                        vehiclesTable.Columns.Add("Дата останнього тех. огляду");
                        vehiclesTable.Columns.Add("Паспорт власника");

                        string[] searchTerms = template.Split(',');

                        foreach (XmlNode node in vehicleNodes)
                        {
                            bool matchFound = true;

                            foreach (string term in searchTerms)
                            {
                                bool termMatch = false;

                                foreach (XmlAttribute attribute in node.Attributes)
                                {
                                    if (attribute.Value.Contains(term.Trim()))
                                    {
                                        termMatch = true;
                                        break;
                                    }
                                }

                                if (!termMatch)
                                {
                                    matchFound = false;
                                    break;
                                }
                            }

                            if (matchFound)
                            {
                                string brand = node.Attributes["Brand"]?.Value;
                                string color = node.Attributes["Color"]?.Value;
                                string factoryNumber = node.Attributes["FactoryNumber"]?.Value;
                                string plateNumber = node.Attributes["PlateNumber"]?.Value;
                                string manufactureDate = node.Attributes["ManufactureDate"]?.Value;
                                string constructionFeatures = node.Attributes["ConstructionFeatures"]?.Value;
                                string painting = node.Attributes["Painting"]?.Value;
                                string lastInspectionDate = node.Attributes["LastInspectionDate"]?.Value;
                                string ownerPassport = node.Attributes["OwnerPassport"]?.Value;

                                vehiclesTable.Rows.Add(brand, color, factoryNumber, plateNumber, manufactureDate, constructionFeatures, painting, lastInspectionDate, ownerPassport);
                            }
                        }

                        DataGridView vehiclesDataGridView = new DataGridView();
                        vehiclesDataGridView.Dock = DockStyle.Fill;
                        vehiclesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        vehiclesDataGridView.DataSource = vehiclesTable;

                        Form vehiclesForm = new Form();
                        vehiclesForm.Text = "Транспортні засоби за шаблоном";
                        vehiclesForm.Size = new Size(1000, 600);
                        vehiclesForm.Controls.Add(vehiclesDataGridView);

                        vehiclesForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Пошук за порожнім шаблоном відмінено.", "Повідомлення");
                    }
                }
                else
                {
                    MessageBox.Show("У файлі немає доданих транспортних засобів.", "Повідомлення");
                }
            }
            else
            {
                MessageBox.Show("Файл не знайдено.", "Помилка");
            }
        }
    }
}
