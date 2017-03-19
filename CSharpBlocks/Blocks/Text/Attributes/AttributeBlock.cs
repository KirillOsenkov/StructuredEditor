using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
    public class AttributeBlock : FocusableLabelBlock
    {
        public AttributeBlock()
        {
            this.MyControl.Focusable = true;
            //this.MyControl.Box.Margins.Left = this.MyControl.CurrentStyle.FontStyleInfo.Font.SpaceCharSize.X;
        }

        protected override string StyleName()
        {
            return "AttributeBlock";
        }

        public static void AddAttributes(EmptyBlock self)
        {
            LanguageService ls = LanguageService.Get(self);
            if (ls == null)
            {
                return;
            }
            if (!(self.Next is ITypeDeclaration) && !(self.Next is IClassLevel))
            {
                return;
            }
            foreach (var attributeType in ls.GetAttributeTypes())
            {
                var name = attributeType.Name.Replace("Attribute", "");
                CreateBlocksItem item = new CreateBlocksItem(
                    name,
                    () => new AttributeBlock() { Text = name }
                );
                item.Picture = Icons.TypeClass;
                self.Completion.Items.Add(item);
            }
        }
    }
}