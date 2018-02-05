using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Security.Cryptography;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using System.Text;
using Windows.Web.Http;
//HEHEHEHHE darrenchanhandsome
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AbsSecure_V1._2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //int euida = 120938123;
        //int euidb = 391341313;
        //int cuida = 483901238;
        //int cuidb = 210184019;
        string empName = "";
        string empID = "";
        string companyID = "";
        string email = "";
        string recipCompanyID = "";

        bool isAbsSecureEnabled = false;

        List<AbsEmailRecord> allEmails;


        public MainPage()
        {
            this.InitializeComponent();
            //Client clientA = new Client(euida, cuida);
            //Client clientB = new Client(euidb, cuidb);
        }

        public string findHash(string str)
        {
            SHA512 shahash = SHA512.Create();
            return BitConverter.ToString(shahash.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "");
        }


        private async void ASend_Click(object sender, RoutedEventArgs e)
        {
            //string encrypted = "";
            //displayBox.Text = "";
            //byte[] stringtoEncrypt = Encoding.BigEndianUnicode.GetBytes(emailContent.Text);

            //AesEnDecryption test = new AesEnDecryption();
            //byte[] bytea = test.Encrypt(stringtoEncrypt);
            //string res = Encoding.BigEndianUnicode.GetString(bytea);
            //encrypted = res;
            //displayBox.Text += @"Encrypted: " + res;
            //bytea = test.Decrypt(bytea);
            //res = Encoding.BigEndianUnicode.GetString(bytea);
            //displayBox.Text += "\n\nDecrypted: " + res;

            //string emailHash = findHash(emailContent.Text);
            //displayBox.Text += "\n\nEmail Hash: " + emailHash;

            if (senderEmail.Text != "" && recpEmail.Text != "" && emailSubj.Text != "" && emailContent.Text != "")
            {
                if (isAbsSecureEnabled)
                {

                    using (HttpClient client = new HttpClient())
                    {
                        var input = new Dictionary<string, string>
                    {
                    { "option", "1" },
                    { "recipEmail", recpEmail.Text},
                    { "senderCompanyID", companyID}
                    };

                        var encodedInput = new HttpFormUrlEncodedContent(input);
                        try
                        {
                            var resp = await client.PostAsync(new Uri("http://evocreate.tk/checkAffiliation.php"), encodedInput);
                            displayBox.Text = resp.Content.ToString();


                            if (resp.StatusCode.Equals(HttpStatusCode.BadRequest))
                            {
                                DisplayDialog("Error!", "Recipient is not a client of AbsSecure!");
                                return;
                            }
                            else
                                recipCompanyID = resp.Content.ToString();
                            input.Add("recipCompanyID", recipCompanyID);
                            input["option"] = "2";
                            encodedInput = new HttpFormUrlEncodedContent(input);
                            var resp2 = await client.PostAsync(new Uri("http://evocreate.tk/checkAffiliation.php"), encodedInput);
                            input["option"] = "3";
                            encodedInput = new HttpFormUrlEncodedContent(input);
                            var resp3 = await client.PostAsync(new Uri("http://evocreate.tk/checkAffiliation.php"), encodedInput);
                            if (!(resp2.StatusCode.Equals(HttpStatusCode.Accepted)) && !(resp3.StatusCode.Equals(HttpStatusCode.Accepted)))
                            {
                                DisplayDialog("ERROR!", "Your corporation is not associated with the recipient!");
                                return;
                            }
                                
                        }
                        catch (Exception)
                        {
                            DisplayDialog("Error!", "Ensure that you have internet connectivity!");
                        }
                    }

                    AesEnDecryption mediumObj = new AesEnDecryption();
                    string encryptedContent = Encoding.BigEndianUnicode.GetString(mediumObj.Encrypt(Encoding.BigEndianUnicode.GetBytes(emailContent.Text)));
                    using (HttpClient client = new HttpClient())
                    {
                        var input = new Dictionary<string, string>
                    {
                    { "senderEmail", senderEmail.Text },
                    { "recipientEmail", recpEmail.Text },
                    { "subject", emailSubj.Text },
                    { "emailContent", encryptedContent },
                    { "senderEmpID", empID },
                    { "symmKey", mediumObj.AES_Key },
                    { "emailHash", getSHA256Hash(encryptedContent)},
                    { "absMailUID", getSHA256Hash(DateTime.Now.ToString()) }
                    };

                        var encodedInput = new HttpFormUrlEncodedContent(input);
                        try
                        {
                            var resp = await client.PostAsync(new Uri("http://evocreate.tk/sendAbsSecureMail.php"), encodedInput);
                        }
                        catch (Exception)
                        {
                            DisplayDialog("Error!", "Ensure that you have internet connectivity!");
                        }
                    }
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var input = new Dictionary<string, string>
                    {
                    { "senderEmail", senderEmail.Text },
                    { "recipientEmail", recpEmail.Text },
                    { "subject", emailSubj.Text },
                    { "emailContent", emailContent.Text }
                    };

                        var encodedInput = new HttpFormUrlEncodedContent(input);
                        try
                        {
                            var resp = await client.PostAsync(new Uri("http://evocreate.tk/sendNormalMail.php"), encodedInput);
                            displayBox.Text = resp.Content.ToString();
                            if (resp.StatusCode.Equals(HttpStatusCode.Ok))
                                DisplayDialog("Success!", "Normal email sent sucessfully!");
                            else
                                DisplayDialog("Failed!", "Email was not sent!");
                        }
                        catch (Exception)
                        {
                            DisplayDialog("Error!", "Please ensure you have internet connectivity!");
                        }
                    }
                }
            }


        }

        public async void Authentication(string u, string p)
        {
            using (HttpClient client = new HttpClient())
            {
                var input = new Dictionary<string, string>
                    {
                    { "email", u },
                    { "password", p }
                    };

                var encodedInput = new HttpFormUrlEncodedContent(input);
                try
                {
                    var resp = await client.PostAsync(new Uri("http://evocreate.tk/login.php"), encodedInput);
                    string respString = resp.Content.ToString();

                    if (resp.StatusCode.Equals(HttpStatusCode.Ok))
                    {
                        email = clientEmail.Text;
                        HideUnhideLogin(false);
                        HideUnhideEverything(true);
                        DisplayDialog("Success!", "You have successfully logged in!");
                        List<string> tmpList = new List<string>();
                        respString = respString.Replace("<br/>", "|");
                        tmpList = respString.Split('|').ToList();
                        empName = tmpList[0];
                        empID = tmpList[1];
                        companyID = tmpList[2];
                        clientInfoText.Text = $"Welcome, {empName}, EmpID: ({empID}), CompanyID: ({companyID})";
                        senderEmail.Text = email;
                    }
                    else if (resp.StatusCode.Equals(HttpStatusCode.Forbidden))
                    {
                        DisplayDialog("Error!", "Password Mismatch!");
                    }
                    else if (resp.StatusCode.Equals(HttpStatusCode.BadRequest))
                    {
                        DisplayDialog("Error!", "No Such Client!");
                    }
                    else
                    {
                        DisplayDialog("Error!", "Request timed out!");
                    }

                }
                catch (Exception)
                {
                    DisplayDialog("Error! Failed to connect to server!", "Ensure that you have internet connectivity!");
                }
            }
        }

        public void HideUnhideLogin(bool b)
        {
            Visibility final = b ? Visibility.Visible : Visibility.Collapsed;
            clientEmail.Text = "";
            clientPassword.Password = "";
            loginText.Visibility = final;
            clientEmail.Visibility = final;
            clientPassword.Visibility = final;
            loginButton.Visibility = final;
        }

        public void HideUnhideEverything(bool b)
        {
            Visibility final = b ? Visibility.Visible : Visibility.Collapsed;
            receiveButton.Visibility = final;
            emailContent.Visibility = final;
            displayBox.Visibility = final;
            senderEmail.Visibility = final;
            recpEmail.Visibility = final;
            emailSubj.Visibility = final;
            emailContent.Visibility = final;
            sendButton.Visibility = final;
            clientInfoText.Visibility = final;
            logOutButton.Visibility = final;
            attachmentContent.Visibility = final;
            emailView.Visibility = final;
            AbsSecureBtn.Visibility = final;
        }

        private async void DisplayDialog(string title, string content)
        {
            ContentDialog Dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await Dialog.ShowAsync();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientEmail.Text != "" && clientPassword.Password != "")
            {
                Authentication(clientEmail.Text, getSHA256Hash(clientPassword.Password));
            }
            else
            {
                DisplayDialog("Error!", "Please enter all relevant fields!");
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            clientInfoText.Text = "";
            emailView.ItemsSource = null;
            senderEmail.Text = "";
            recpEmail.Text = "";
            emailSubj.Text = "";
            emailContent.Text = "";
            HideUnhideEverything(false);
            HideUnhideLogin(true);
            email = "";
        }

        private async void receiveButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var input = new Dictionary<string, string>
                    {
                    { "email", email },
                    };

                var encodedInput = new HttpFormUrlEncodedContent(input);
                try
                {
                    List<string> tmpList;
                    var resp = await client.PostAsync(new Uri("http://evocreate.tk/retrieveMail.php"), encodedInput);
                    if (resp.StatusCode.Equals(HttpStatusCode.NotFound))
                        emailView.ItemsSource = new List<string>() { "No email records found." };
                    else if (resp.StatusCode.Equals(HttpStatusCode.Ok))
                    {
                        allEmails = new List<AbsEmailRecord>();
                        string respString = resp.Content.ToString();
                        respString = respString.Replace("<hr/>", "|");
                        respString = respString.Substring(1, respString.Count() - 1);
                        List<string> emailsList = respString.Split('|').ToList();
                        foreach (string s in emailsList)
                        {
                            string snew;
                            snew = s.Replace("<br/>", "|");
                            tmpList = snew.Split("|".ToCharArray()).ToList();
                            if (tmpList[0] == "noAbsMailUID")
                            {
                                AbsEmailRecord aer = new AbsEmailRecord(tmpList[1], tmpList[2], tmpList[3], tmpList[4], tmpList[5]);
                                allEmails.Add(aer);
                            }
                            else
                            {
                                AbsEmailRecord aer = new AbsEmailRecord(tmpList[1], tmpList[2], tmpList[3], tmpList[4], tmpList[5], tmpList[6], true);
                                allEmails.Add(aer);
                            }
                        }
                        emailView.ItemsSource = allEmails;
                    }
                    else
                    {
                        DisplayDialog("Error!", "Request timed out!");
                    }

                }
                catch (Exception)
                {
                    DisplayDialog("Error!", "Ensure that you have internet connectivity!");
                }
            }
        }

        private void AbsSecureBtn_Checked(object sender, RoutedEventArgs e)
        {
            isAbsSecureEnabled = !isAbsSecureEnabled;
        }

        public async void showEmail(string content)
        {
            ContentDialog Email = new ContentDialog
            {
                Title = "Email",
                CloseButtonText = "Close",
                Content = content,
                Width = 1200,
                Height = 1700
            };
            await Email.ShowAsync();
        }

        private void emailView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (emailView.SelectedItem != null)
            {
                AbsEmailRecord aer = (AbsEmailRecord)emailView.SelectedItem;
                showEmail(aer.showFullContent());
            }
        }

        private string getSHA256Hash(string tbh)
        {
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer hashed = hap.HashData(CryptographicBuffer.ConvertStringToBinary(tbh, BinaryStringEncoding.Utf8));
            return CryptographicBuffer.EncodeToHexString(hashed).ToUpper();

        }
    }
}
