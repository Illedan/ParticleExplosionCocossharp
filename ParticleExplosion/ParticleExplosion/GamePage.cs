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
			BackgroundColor = Color.White;
			var container = new Grid ();
			container.RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				new RowDefinition { Height = new GridLength(1, GridUnitType.Star)  },
			};

			var browser = new WebView
			{
				Source = "https://google.com",
				WidthRequest = 200,
				HeightRequest = 200,
			};

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
				ViewCreated = LoadGame,
				BackgroundColor = Color.Transparent
			};

			container.Children.Add(browser,0,0);
			container.Children.Add(textField, 0,1);
			container.Children.Add(gameView);
			Grid.SetRowSpan(gameView, 2);

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
