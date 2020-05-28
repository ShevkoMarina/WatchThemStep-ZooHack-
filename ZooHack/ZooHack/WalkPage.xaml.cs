using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.AudioRecorder;
using Xamarin.Forms.Xaml;


namespace ZooHack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkPage : ContentPage
    {
        static int counter1 = 0;
        static int counter2 = 0;
        static int counter3 = 0;
        AudioRecorderService recorder;

        SpeechConfig speechConfig1 = SpeechConfig.FromSubscription("b7ae8e587d1f4026943894d0e618761e", "eastus");
        public WalkPage()
        {

            InitializeComponent();

            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(5),
                
            };      
        }
        async void Record_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Microphone))
                    {
                         await DisplayAlert("Need microphone", "I need permition", "OK");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Microphone);
                    if (results.ContainsKey(Permission.Microphone))
                    {
                        status = results[Permission.Microphone];
                    }
                }
                else
                {
                    await RecordAudio();
                   
                }
            }     
            catch(Exception ex)
            {
                await DisplayAlert("Произошла ошибка",ex.Message,"OK");
            }
        }

        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording) //Record button clicked
                {
                    RecordButton.IsEnabled = false;

                    PlayButton.IsEnabled = false;

                    //start recording audio

                    var audioRecordTask = await recorder.StartRecording();

                    RecordButton.Text = "Закончить запись";

                    RecordButton.IsEnabled = true;
                    await audioRecordTask;

                    RecordButton.Text = "Начать запись";
                    PlayButton.IsEnabled = true;

                }

                else //Stop button clicked
                {
                    RecordButton.IsEnabled = false;
                    //stop the recording...
                    await recorder.StopRecording();
                    RecordButton.IsEnabled = true;
                    await PlayAudio();
                }
            }

            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }
        async Task PlayAudio()
        {        
            try
            {
                var filePath = recorder.GetAudioFilePath();
               
                if (filePath != null)
                {
                    PlayButton.IsEnabled = false;
                    RecordButton.IsEnabled = false;
                    speechConfig1.SpeechRecognitionLanguage = "ru-RU";
                    using (var audioInput = AudioConfig.FromWavFileInput(filePath))
                    {
                        using (var recognizer = new SpeechRecognizer(speechConfig1, audioInput))
                        {
        
                            //await DisplayAlert("Results", "Recognizing first result...", "OK");
                            var result = await recognizer.RecognizeOnceAsync();

                            if (result.Reason == ResultReason.RecognizedSpeech)
                            {
                                //await DisplayAlert("Results", $"We recognized: {result.Text}", "OK");
                                if (result.Text == "Белка +1?")
                                {
                                    counter1++;                              
                                    Belka.Text = $"Белка: {counter1}";
                                }
                                if (result.Text == "Волк +1?")
                                {
                                    counter2++;
                                    Volk.Text = $"Волк: {counter2}";
                                }
                                if (result.Text == "Росомаха +1?")
                                {
                                    counter3++;
                                    Rosomaha.Text = $"Росомаха: {counter3}";
                                }                            
                               
                            }
                            else if (result.Reason == ResultReason.NoMatch)
                            {
                                await DisplayAlert("Results", $"NOMATCH: Speech could not be recognized.", "OK");
                            }
                            else if (result.Reason == ResultReason.Canceled)
                            {
                                var cancellation = CancellationDetails.FromResult(result);
                                await DisplayAlert("Results", $"CANCELED: Reason={cancellation.Reason}", "OK");

                                if (cancellation.Reason == CancellationReason.Error)
                                {
                                    await DisplayAlert("Error", $"CANCELED: ErrorCode={cancellation.ErrorCode}", "OK");
                                    await DisplayAlert("Error", $"ErrorDetails ={cancellation.ErrorDetails}", "OK");

                                }
                            }
                            PlayButton.IsEnabled = true;
                            RecordButton.IsEnabled = true;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                //blow up the app!
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            PlayButton.IsEnabled = true;
            RecordButton.IsEnabled = true;
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(); 
        }
    }
}
