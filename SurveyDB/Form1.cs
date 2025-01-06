using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace survey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DatabaseConnection database = new DatabaseConnection();

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            string firstName = textBoxImie.Text;
            string lastName = textBoxNazwisko.Text;
            string answer1 = textBoxOdp1.Text;
            string answer2 = textBoxOdp2.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(answer1) || string.IsNullOrEmpty(answer2))
            {
                MessageBox.Show("All fields must be filled!");
                return;
            }

            AddToDatabase(firstName, lastName, answer1, answer2);
        }

        private void AddToDatabase(string firstName, string lastName, string answer1, string answer2)
        {
            using (var connection = new SQLiteConnection(DatabaseConnection.DATABASE_CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "INSERT INTO Person (FirstName, LastName) VALUES (@FirstName, @LastName)";
                            command.Parameters.AddWithValue("@FirstName", firstName);
                            command.Parameters.AddWithValue("@LastName", lastName);
                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT last_insert_rowid()";
                            long personId = (long)command.ExecuteScalar();

                            command.CommandText = "INSERT INTO Survey (PersonId, Answer1, Answer2) VALUES (@PersonId, @Answer1, @Answer2)";
                            command.Parameters.AddWithValue("@PersonId", personId);
                            command.Parameters.AddWithValue("@Answer1", answer1);
                            command.Parameters.AddWithValue("@Answer2", answer2);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }

                    MessageBox.Show("Data successfully saved to the database!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error saving data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ShowDataInDataGridView()
        {
            using (var connection = new SQLiteConnection(DatabaseConnection.DATABASE_CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT Person.FirstName, Person.LastName, Survey.SurveyId, Survey.Answer1, Survey.Answer2
                    FROM Person
                    INNER JOIN Survey ON Person.PersonId = Survey.PersonId";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Columns["SurveyId"].HeaderText = "Survey Number";
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error retrieving data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void buttonOdswiez_Click(object sender, EventArgs e)
        {
            ShowDataInDataGridView();
        }
    }
}
