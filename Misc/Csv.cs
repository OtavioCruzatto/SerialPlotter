using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SerialPlotter.Misc
{
    public class Csv
    {
        private string delimiter;
        private int qtyOfColumns;
        private int qtyOfRows;
        private bool dataIsPrepared;
        private bool dataIsReady;

        private List<int[]> columns;
        private List<string> columnDataNames;
        private List<string> rows;

        public Csv()
        {
            this.delimiter = ";";
            this.qtyOfColumns = 0;
            this.qtyOfRows = 0;
            this.dataIsPrepared = false;

            this.columns = new List<int[]>();
            this.columnDataNames = new List<string>();
            this.rows = new List<string>();
        }

        public void SetColumnData(int[] data, string dataName)
        {
            this.columnDataNames.Add(dataName);
            this.columns.Add(data);
            this.qtyOfColumns++;
            this.dataIsPrepared = false;
        }

        public void PrepareData()
        {
            this.SetNamesToDataRows();
            this.SetDataToDataRows();
            this.dataIsPrepared = true;
        }

        private void SetNamesToDataRows()
        {
            string firstRow = "";
            for (int i = 0; i < this.columnDataNames.Count; i++)
            {
                firstRow += this.columnDataNames[i] + this.delimiter;
            }
            string sanatizedFirstRow = firstRow.Remove(firstRow.LastIndexOf(this.delimiter));
            this.rows.Add(sanatizedFirstRow);
        }

        private void SetDataToDataRows()
        {
            string data = "";
            this.qtyOfRows = this.columns[0].Length;
            for (int row = 0; row < this.qtyOfRows; row++)
            {
                for (int column = 0; column < this.qtyOfColumns; column++)
                {
                    data += this.columns[column][row].ToString() + this.delimiter;
                }

                string sanatizedData = data.Remove(data.LastIndexOf(this.delimiter));
                this.rows.Add(sanatizedData);
                data = "";
            }
        }

        public void ShowFormattedData()
        {
            if (this.dataIsPrepared)
            {
                foreach (string row in this.rows)
                {
                    Console.WriteLine(row);
                }
            }
        }

        public void SaveDataToFile()
        {
            if (this.dataIsPrepared == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Select a directory to save the file...";
                saveFileDialog.InitialDirectory = @"C:\";
                saveFileDialog.Filter = "CSV Files | *.csv";
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string pathName = saveFileDialog.FileName;

                        if (File.Exists(pathName))
                        {
                            File.Delete(pathName);
                        }

                        FileStream fileStream = new FileStream(pathName, FileMode.OpenOrCreate);
                        StreamWriter streamWriter = new StreamWriter(fileStream);

                        for (int i = 0; i < (this.qtyOfRows + 1); i++)
                        {
                            streamWriter.WriteLine(this.rows[i]);
                        }

                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Data is not prepared to be saved.", "Data save error.");
            }
        }

        public void ReadDataFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select file...";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "CSV Files | *.csv";
            openFileDialog.DefaultExt = "csv";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string pathName = openFileDialog.FileName;

                    if (File.Exists(pathName))
                    {
                        this.Clear();

                        this.qtyOfRows = File.ReadLines(pathName).Count() - 1;
                        this.qtyOfColumns = File.ReadLines(pathName).First().Split(this.delimiter[0]).Length;

                        for (int i = 0; i < this.qtyOfColumns; i++)
                        {
                            this.columns.Add((int[])Array.CreateInstance(typeof(int), this.qtyOfRows));
                        }

                        FileStream fileStream = new FileStream(pathName, FileMode.Open);
                        StreamReader streamReader = new StreamReader(fileStream);

                        int currentRow = 0;
                        int currentColumn = 0;

                        while (streamReader.EndOfStream == false)
                        {
                            string line = streamReader.ReadLine();
                            string[] sanatizedLine = line.Trim().Split(this.delimiter[0]);

                            foreach (string s in sanatizedLine)
                            {
                                if (int.TryParse(s, out int result))
                                {
                                    this.columns[currentColumn][currentRow] = result;

                                    currentColumn++;
                                    if (currentColumn > (this.qtyOfColumns - 1))
                                    {
                                        currentColumn = 0;
                                        currentRow++;
                                    }
                                }
                                else
                                {
                                    this.columnDataNames.Add(s);
                                }
                            }
                        }
                        streamReader.Close();
                        this.dataIsReady = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public List<int[]> GetDataColumns()
        {
            this.ReadDataFromFile();
            return this.columns;
        }

        public int GetQtyOfRows()
        {
            return this.qtyOfRows;
        }

        public int GetQtyOfColumns()
        {
            return this.qtyOfColumns;
        }

        public void Clear()
        {
            this.qtyOfColumns = 0;
            this.qtyOfRows = 0;
            this.dataIsPrepared = false;
            this.columns.Clear();
            this.columnDataNames.Clear();
            this.rows.Clear();
        }

        public void SetDelimiter(string delimiter)
        {
            this.delimiter = delimiter;
        }

        public bool GetDataReady()
        {
            return this.dataIsReady;
        }
    }
}
