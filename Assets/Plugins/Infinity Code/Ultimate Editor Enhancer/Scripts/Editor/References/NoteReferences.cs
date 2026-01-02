/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using InfinityCode.UltimateEditorEnhancer.PostHeader;

namespace InfinityCode.UltimateEditorEnhancer.References
{
    public class NoteReferences : ReferenceBase<NoteReferences, NoteItem>
    {
        protected override string filename => "Notes";

        protected override void OnResetContent()
        {
            
        }
    }
}