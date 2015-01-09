using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    //        "type" = {
    //    conditionType = 1,  --条件类型 1.等级 2.关卡结束
    //    condition = "10",	--条件内容 
    //},


    //[index] = {
    //    posX = 100,        --高亮框位置x
    //    posY = 100,		   --高亮框位置y
    //    width = 100,		--框大小
    //    height = 100,
    //    hintX = 100,       --提示位置x
    //    hintY = 100,		--提示位置y
    //    hintContent = "内容内容内容"  --提示内容
    //    animationType = 1, 	--动画收拾类型 1,动画 2 移动手势
    //    fromX = 100,	--从动画位置x
    //    fromY = 100,   --从动画位置y
    //    toX = 200,		--到动画位置x
    //    toY = 200,		--到动画位置y
    //    animationRoation = 20,
    //    registerMessageNext = "",

    //},

    public class GuideConfigExporter
    {
        private const int MAX_GUIDE = 20;

        public StringBuilder exportLuaConfig()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("GKSteps = {");

            for (int i = 1; i < MAX_GUIDE; i++)
            {
                generateOneGuide(i, ref sb);
            }

            sb.AppendLine(" };");

            return sb;
        }

        private void generateOneGuide(int guideIndex,ref StringBuilder sb)
        {
            // 步骤
            IEnumerable<Guide> steps = from config in DBConfigMgr.Instance.MapGuide
                                       where config.Value.GuideID == guideIndex
                                       select config.Value;

            if (steps != null && steps.Count() > 0)
            {
                // 排序
                steps.OrderBy(x => x.Step);

                string[] triggers = steps.ElementAt(0).Trigger.Split(',');
                sb.AppendFormat("\t[\"Guide{0}\"] = {{" + Environment.NewLine, guideIndex);
                sb.AppendLine("\t\t[\"type\"] = {");
                sb.AppendFormat("\t\t\tconditionType = {0},  " + Environment.NewLine, triggers[0]);
                sb.AppendFormat("\t\t\tcondition = {0},	" + Environment.NewLine, triggers[1]);
                sb.AppendLine("\t\t},");
                sb.AppendLine("");

                int currentStepIndex = 1;
                foreach (Guide g in steps)
                {
                    if (g.UIEffect != null && g.UIEffect.Length > 0 && g.OnUI != "战斗界面")
                    {
                        generateOneStep(currentStepIndex,g, ref sb);
                        currentStepIndex++;
                    }
                }

                sb.AppendLine("\t},");
            }
        }

        private void generateOneStep(int stepIndex,Guide g, ref StringBuilder sb)
        {
            int animateType = 1;
            if (g.FinishType == "C2") animateType = 1;
            if (g.FinishType == "C3") animateType = 2;

            sb.AppendFormat("\t\t[{0}] = {{" + Environment.NewLine, stepIndex);
            sb.AppendFormat("\t\t\tposX = {0}," + Environment.NewLine, g.X);
            sb.AppendFormat("\t\t\tposY = {0}," + Environment.NewLine, g.Y);
            sb.AppendFormat("\t\t\twidth = {0}," + Environment.NewLine, g.W);
            sb.AppendFormat("\t\t\theight = {0}," + Environment.NewLine, g.H);
            sb.AppendFormat("\t\t\tposX2 = {0}," + Environment.NewLine, g.X2);
            sb.AppendFormat("\t\t\tposY2 = {0}," + Environment.NewLine, g.Y2);
            sb.AppendFormat("\t\t\twidth2 = {0}," + Environment.NewLine, g.W2);
            sb.AppendFormat("\t\t\theight2 = {0}," + Environment.NewLine, g.H2);
            sb.AppendFormat("\t\t\thintX = {0}," + Environment.NewLine, g.HintX);
            sb.AppendFormat("\t\t\thintY = {0}," + Environment.NewLine, g.HintY);
            sb.AppendFormat("\t\t\thintContent = \"{0}\"," + Environment.NewLine, g.HintText);
            sb.AppendFormat("\t\t\tanimationType = {0}," + Environment.NewLine, animateType);
            sb.AppendFormat("\t\t\tfromX = {0}," + Environment.NewLine, g.FingerX);
            sb.AppendFormat("\t\t\tfromY = {0}," + Environment.NewLine, g.FingerY);
            sb.AppendFormat("\t\t\ttoX = {0}," + Environment.NewLine, g.FingerX2);
            sb.AppendFormat("\t\t\ttoY = {0}," + Environment.NewLine, g.FingerY2);
            sb.AppendFormat("\t\t\tanimationRoation = {0}," + Environment.NewLine, 0);
            sb.AppendFormat("\t\t\tregisterMessageNext = {0}," + Environment.NewLine, "GuideCommonEvent.COMMON_CLICK_EVENT");

            sb.AppendLine("\t\t},");
        }

    }
}
