﻿using Blitline.Net.ParamOptions;

namespace Blitline.Net.Functions
{
    /// <summary>
    /// Resize to fit, but will pad to keep the aspect ratio. 
    /// So for example, if you are going from a 3:4 aspect ratio to a 3:2 aspect ratio, 
    /// this method will assure the result the desired output size, and pad the filled in area with a specified color.
    /// </summary>
    public class PadResizeToFitFunction : BlitlineFunction
    {
        public override string Name
        {
            get { return "pad_resize_to_fit"; }
        }

        public override object Params { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">Output width</param>
        /// <param name="height">Output height</param>
        /// <param name="colour">Color for the padding (defaults to "#ffffff")</param>
        /// <param name="gravity">Location of output relative to padding (defaults to CenterGrativty)</param>
        public PadResizeToFitFunction(int width, int height, string colour = "#ffffff", Gravity gravity = Gravity.CenterGrativty)
        {
            @Params = new
                {
                    width,
                    height,
                    color = colour,
                    gravity
                };
        }
    }
}