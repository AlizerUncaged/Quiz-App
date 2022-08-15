using System.Collections.ObjectModel;

namespace Quiz_App.Model
{
    public class Player
    {
        public ObservableCollection<float> CurrentHealths { get; } = new ObservableCollection<float>()
        {
            1f, 1f, 1f, 1f
        };


        public double CurrentPoints { get; set; } = 100;

        public double HighestScore { get; set; } = 0;
    }
}