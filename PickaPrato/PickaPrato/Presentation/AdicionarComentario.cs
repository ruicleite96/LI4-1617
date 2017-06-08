﻿﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PickaPrato.Business;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "AdicionarComentario")]


    public class AdicionarComentario : DialogFragment {

        private Button cancelarButton;
        private Button partilharButton;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            var view = inflater.Inflate(Resource.Layout.AdicionarComentario, container, true);

            cancelarButton = view.FindViewById<Button>(Resource.Id.cancelar);
            cancelarButton.Click += Button_Dismiss_Click;

            EditText comentario = view.FindViewById<EditText>(Resource.Id.comentario);
            RatingBar classificacao = view.FindViewById<RatingBar>(Resource.Id.classificacao); 
            Switch partilhar = view.FindViewById<Switch>(Resource.Id.switchpartilhar);

            partilharButton = view.FindViewById<Button>(Resource.Id.partilhar);
            partilharButton.Click += (sender, e) => {
                Facade.AdicionarClassificacao(comentario.Text, Convert.ToInt32(classificacao.Rating), 
                                              DescricaoPrato.pratosel.IdPrato);
            };

			return view;
		}

		private void Button_Dismiss_Click(object sender, EventArgs e) {
			Dismiss();
		}

        public override void OnResume() {
            Dialog.Window.SetLayout(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);

            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));

            SetStyle(DialogFragmentStyle.NoTitle, Android.Resource.Style.Theme);
            
            base.OnResume();
        }

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);

			// Unwire event
            if (disposing) {
                cancelarButton.Click -= Button_Dismiss_Click;
            }
		}
	}
}