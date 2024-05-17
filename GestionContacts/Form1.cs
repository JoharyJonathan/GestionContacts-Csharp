using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace GestionContacts
{
    public partial class Form1 : Form
    {
        private List<Person> people = new List<Person>();

        public Form1()
        {
            InitializeComponent();
            ConfigureDataGridView();
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = textBox1.Text;
                string prenom = textBox2.Text;
                string number = textBox3.Text;

                Person newPerson = new Person(nom, prenom, number);

                people.Add(newPerson);

                string json = JsonConvert.SerializeObject(people, Formatting.Indented);
                File.WriteAllText(@"e:\C# projects\GestionContacts\GestionContacts\data\data.json", json);

                MessageBox.Show("Données enregistrées avec succès !");
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}\n{ex.InnerException?.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un contact à supprimer.");
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedContact = (Person)selectedRow.DataBoundItem;

                string filePath = @"e:\C# projects\GestionContacts\GestionContacts\data\data.json";
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    people = JsonConvert.DeserializeObject<List<Person>>(json);

                    Person personToRemove = people.FirstOrDefault(p => p.Nom == selectedContact.Nom && p.Prenom == selectedContact.Prenom && p.Number == selectedContact.Number);
                    if (personToRemove != null)
                    {
                        people.Remove(personToRemove);

                        json = JsonConvert.SerializeObject(people, Formatting.Indented);
                        File.WriteAllText(filePath, json);

                        loadData();

                        MessageBox.Show("Contact supprimé avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("Le contact n'a pas été trouvé dans la liste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}\n{ex.InnerException?.Message}");
            }
        }

        public void loadData()
        {
            try
            {
                if (File.Exists(@"e:\C# projects\GestionContacts\GestionContacts\data\data.json"))
                {
                    string json = File.ReadAllText(@"e:\C# projects\GestionContacts\GestionContacts\data\data.json");
                    people = JsonConvert.DeserializeObject<List<Person>>(json);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = people;
                }
                else
                {
                    MessageBox.Show("Aucune donnée trouvée.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}\n{ex.InnerException?.Message}");
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        }
    }
}