using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace MobileDevelopmentProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText name, uname, password;
        EditText street, city, postalcode, phone;

        TextView ageText;
        TextView hoursText, feesText, totalHoursText, totalFeesText, totalFeesTextTax;

        Button signin;
        Button submit;
        Button addCourse, submitThird;

        Spinner courses;

        List<string> courseNames, choosenCourse;
        int[] courseHours, courseFees;
        int totalHourscount, totalFeescount;

        RadioButton graduated;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_main);

            name = FindViewById<EditText>(Resource.Id.editTextName);
            uname = FindViewById<EditText>(Resource.Id.editTextUName);
            password = FindViewById<EditText>(Resource.Id.editTextPassword);

            signin = FindViewById<Button>(Resource.Id.buttonSignIn);
            signin.Click += delegate
            {

                if (!name.Text.Equals("") && !uname.Text.Equals("") && !password.Text.Equals(""))
                {

                    if (uname.Text.ToUpper().Equals("STUDENT1") && password.Text.Equals("123456"))
                    {
                        SetContentView(Resource.Layout.content_second);
                        initSecondPage();

                    }
                    else
                    {
                        Toast.MakeText(this, "Wrong username or password", ToastLength.Long).Show();
                    }

                }
                else
                {
                    Toast.MakeText(this, "All fields are required", ToastLength.Long).Show();
                }

            };



        }

        private void initSecondPage()
        {
            TextView welcome = FindViewById<TextView>(Resource.Id.welcome);
            welcome.Text += name.Text;

            street = FindViewById<EditText>(Resource.Id.editTextStreet);
            city = FindViewById<EditText>(Resource.Id.editTextCity);
            postalcode = FindViewById<EditText>(Resource.Id.editTextPostal);
            phone = FindViewById<EditText>(Resource.Id.editTextPhone);

            graduated = FindViewById<RadioButton>(Resource.Id.radioButton1);

            ageText = FindViewById<TextView>(Resource.Id.textview2);
            SeekBar ageBar = FindViewById<SeekBar>(Resource.Id.seekBarAge);
            ageBar.ProgressChanged += (sender, e) =>
            {
                if (e.FromUser)
                {
                    ageText.Text = ageBar.Progress.ToString();
                }
            };

            submit = FindViewById<Button>(Resource.Id.buttonSubmit);
            submit.Click += delegate
            {

                if (ageBar.Progress >= 15)
                {
                    if (!street.Text.Equals("") && !city.Text.Equals("") &&
                    !postalcode.Text.Equals("") && !phone.Text.Equals(""))
                    {

                        SetContentView(Resource.Layout.content_third);
                        initThirdPage();
                    }
                    else
                    {
                        Toast.MakeText(this, "All fields are required", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Age cannot be lesser than 15", ToastLength.Long).Show();
                }
            };
        }

        private void initThirdPage()
        {
            courses = FindViewById<Spinner>(Resource.Id.spinnerCourse);
            courses.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerSelected);

            courseNames = new List<string>() { "Mobile development", "Project management", "Emerging technology", "Co-op", "Database", "Cloud computing", "Big data", "Java", "Paython", "Business" };
            courseHours = new int[] { 3, 4, 5, 6, 3, 4, 5, 6, 3, 4 };
            courseFees = new int[] { 900, 1000, 1100, 1200, 1300, 1400, 1500, 1100, 900, 1000 };

            choosenCourse = new List<string>();

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, courseNames);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            courses.Adapter = adapter;

            hoursText = FindViewById<TextView>(Resource.Id.courseHours);
            feesText = FindViewById<TextView>(Resource.Id.courseFees);
            totalHoursText = FindViewById<TextView>(Resource.Id.textViewTotalhours);
            totalFeesText = FindViewById<TextView>(Resource.Id.textViewTotalfees);

            addCourse = FindViewById<Button>(Resource.Id.buttonAddcourse);
            addCourse.Click += delegate
            {

                if (!choosenCourse.Contains(courses.SelectedItem.ToString()))
                {

                    if ((totalHourscount + courseHours[courses.SelectedItemPosition]) <= (graduated.Checked ? 21 : 17))
                    {

                        choosenCourse.Add(courses.SelectedItem.ToString());

                        totalHourscount += courseHours[courses.SelectedItemPosition];
                        totalFeescount += courseFees[courses.SelectedItemPosition];

                        totalHoursText.Text = "Total hours: " + totalHourscount.ToString();
                        totalFeesText.Text = "Total fees: $" + totalFeescount.ToString();
                    }
                    else
                    {
                        Toast.MakeText(this, "Maximum hours allowed is " + (graduated.Checked ? 21 : 17).ToString(), ToastLength.Long).Show();
                    }

                }
                else
                {
                    Toast.MakeText(this, "Course already added", ToastLength.Long).Show();
                }

            };

            submitThird = FindViewById<Button>(Resource.Id.buttonSubmitThird);
            submitThird.Click += delegate
            {

                SetContentView(Resource.Layout.content_fourth);
                initFourthPage();
            };

        }

        private void initFourthPage()
        {
            FindViewById<TextView>(Resource.Id.studentname).Text += name.Text;

            CheckBox accomodation = FindViewById<CheckBox>(Resource.Id.checkBox1);
            CheckBox transportation = FindViewById<CheckBox>(Resource.Id.checkBox2);
            CheckBox books = FindViewById<CheckBox>(Resource.Id.checkBox3);

            accomodation.CheckedChange += (sender, e) =>
            {
                if (e.IsChecked)
                {
                    totalFeescount += 3000;
                }
                else
                {
                    totalFeescount -= 3000;
                }

                setFees();
            };

            transportation.CheckedChange += (sender, e) =>
            {
                if (e.IsChecked)
                {
                    totalFeescount += 1000;
                }
                else
                {
                    totalFeescount -= 1000;
                }

                setFees();
            };

            books.CheckedChange += (sender, e) =>
            {
                if (e.IsChecked)
                {
                    totalFeescount += 600;
                }
                else
                {
                    totalFeescount -= 600;
                }

                setFees();
            };

            FindViewById<TextView>(Resource.Id.textViewTotalhoursfinal).Text = "Total hours: " + totalHourscount.ToString();
            totalFeesTextTax = FindViewById<TextView>(Resource.Id.textViewTotalfeesfinal);
            totalFeesTextTax.Text = "Total fees(+Tax): " + (totalFeescount * 1.13).ToString();

            FindViewById<Button>(Resource.Id.finishButton).Click += delegate
            {
                Toast.MakeText(this, "Enrolled successfully", ToastLength.Long).Show();

            };

        }

        private void setFees()
        {
            totalFeesTextTax.Text = "Total fees(+Tax): $" + (totalFeescount * 1.13).ToString();
        }

        private void spinnerSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            hoursText.Text = "Course hours: " + courseHours[e.Position].ToString();
            feesText.Text = "Course fees: $" + courseFees[e.Position].ToString();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

