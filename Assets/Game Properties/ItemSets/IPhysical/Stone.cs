using System.Collections;
using System.Collections.Generic;
using Game_Properties.ItemSets;
using Game_Properties.ItemSets.interfaces;
using UnityEngine;

namespace Game_Properties.ItemSets.IPhysical {

    public class Stone : Block {

        public override int Value {
            get {
                return 0x100;
            }
            set {
                this.Value = value;
            }
        }

        protected override string Name {
            get {
                return this.GetType().Name;
            }
        }

        public override ItemHandler Handler {
            get {
                return new StoneBlockHandler(this);
            }
        }

        public override Color32 Color {
            get {
                return UnityEngine.Color.gray;
            }
        }
    }

    public class StoneBlockHandler : ItemHandler {

        public StoneBlockHandler(INItem Class) : base(Class) {

        }

    }

}
