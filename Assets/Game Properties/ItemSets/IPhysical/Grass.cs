using Game_Properties.ItemSets.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Properties.ItemSets.IPhysical {


    public class Grass : Block {

        protected override string Name {
            get {
                return this.GetType().Name;
            }
        }

        public override ItemHandler Handler {
            get {
                return new GrassBlockHandler(this);
            }
        }

        public override int Value {
            get {
                return 0x300;
            }
            set {
                this.Value = value;
            }
        }

        public override Color32 Color {
            get {
                return UnityEngine.Color.green;
            }
        }
    }



    public class GrassBlockHandler : ItemHandler {

        public GrassBlockHandler(INItem Class) : base(Class) {

        }

    }
}
