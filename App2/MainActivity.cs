using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using App2.Resources.DataHelper;
using App2.Resources.Model;
using System.Collections.Generic;

namespace App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ListView lstViewData;
        List<Product> listSource = new List<Product>();
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);
            //Create Database  
            db = new Database();
            db.createDatabase();
            lstViewData = FindViewById<ListView>(Resource.Id.listView);
            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnRemove = FindViewById<Button>(Resource.Id.btnRemove);
            //Load Data  
            LoadData();
            //Event  
            btnAdd.Click += delegate
            {
                Product product = new Product()
                {
                    Name = edtName.Text,
                    
                };
                db.insertIntoTable(product);
                LoadData();
            };
            btnEdit.Click += delegate
            {
                Product product = new Product()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text
                };
                db.updateTable(product);
                LoadData();
            };
            btnRemove.Click += delegate
            {
                Product product = new Product()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text
                };
                db.removeTable(product);
                LoadData();
            };
            lstViewData.ItemClick += (s, e) =>
            {
                //Set Backround for selected item  
                for (int i = 0; i < lstViewData.Count; i++)
                {
                    if (e.Position == i)
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black);
                    else
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                //Binding Data  
                var txtName = e.View.FindViewById<TextView>(Resource.Id.txtView_Name);
                
                
                edtName.Tag = e.Id;
                
            };
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

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        
        
            
            private void LoadData()
            {
                listSource = db.selectTable();
                var adapter = new ListViewAdapter(this, listSource);
                lstViewData.Adapter = adapter;
            }
        
    }
}

