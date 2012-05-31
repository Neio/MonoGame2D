using System.Xml.Serialization;
namespace MonoGame2D.Particle {

    ///<summary>Groups from-to values into one pair</summary>
    public struct Range {
        [XmlAttribute]
        public float From;
        [XmlAttribute]
        public float To;

        public float Difference {
            get { return To - From; }
        }

        public float Average {
            get { return (From + To) * 0.5f; }
        }

        public Range(float from, float to) {
            From = from;
            To = to;
        }

        public Range(float fromTo) {
            From = fromTo;
            To = fromTo;
        }

        public float PickRangeValue(float ratio) {
            return From + Difference * ratio;
        }
    }
}