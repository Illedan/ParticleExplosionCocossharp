using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CocosSharp;

namespace ParticleExplosion
{
	public class GamePage : ContentPage
	{
		private CocosSharpView gameView;

		public GamePage ()
		{
			var container = new Grid ();

			var textField = new Entry (){Text = "Click here to edit", 
				HorizontalOptions = LayoutOptions.Center, 
				VerticalOptions = LayoutOptions.Center ,
				HeightRequest = 50, 
				WidthRequest = 300, 
				BackgroundColor = Color.Transparent};

			gameView = new CocosSharpView () {
				DesignResolution = new Size (Constant.Width, Constant.Height),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				ViewCreated = LoadGame
			};
			container.Children.Add (gameView);	
			container.Children.Add (textField);

			Content = container;
		}

		protected override void OnDisappearing ()
		{
			if (gameView != null) {
				gameView.Paused = true;
			}

			base.OnDisappearing ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (gameView != null)
				gameView.Paused = false;
		}

		void LoadGame (object sender, EventArgs e)
		{
			var nativeGameView = sender as CCGameView;

			if (nativeGameView != null) {
				var contentSearchPaths = new List<string> () { "Fonts", "Sounds", "Images" };
				CCSizeI viewSize = nativeGameView.ViewSize;
				CCSizeI designResolution = nativeGameView.DesignResolution;

				nativeGameView.ContentManager.SearchPaths = contentSearchPaths;

				CCScene gameScene = new CCScene (nativeGameView);
				gameScene.AddLayer (new GameLayer ());
				nativeGameView.RunWithScene (gameScene);
			}
		}
	}
}
