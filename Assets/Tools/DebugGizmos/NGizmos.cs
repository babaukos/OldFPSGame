//////////////////////////////////////////////////////
//               Расширений клас гизмо              //
//   позволяет отрисовыват гизмо с доп параметрами  //
//                     ver. 0.3                     //
//                                                  //
//                  Michael Khmelevsky              //
//////////////////////////////////////////////////////

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Custom.Utility
{
    public static class NGizmos
    {
        public enum PointTipe
        {
            cube,
            crist,
            sphere,
            all,
        }
        //
        public static void DrawVisor(Vector3 pos,float fov, float maxRnage, float minRange, float spect)
        {
            Gizmos.DrawFrustum(pos, fov, maxRnage, minRange, spect);
        }

        //
        public static void DrawGizmoArrow(float xPos, float zPos)
        {
            Gizmos.color = Color.green;
            float arrowHeight = 300;
            Vector3 arrowStart = new Vector3(xPos, arrowHeight, zPos - 200);
            Vector3 arrowEnd = new Vector3(xPos, arrowHeight, zPos + 200);
            Gizmos.DrawLine(arrowStart, arrowEnd);
            Vector3 crossLineStart = new Vector3(xPos - 50, arrowHeight, zPos + 50);
            Vector3 crossLineEnd = new Vector3(xPos + 50, arrowHeight, zPos + 50);
            Gizmos.DrawLine(crossLineStart, crossLineEnd);
            Gizmos.DrawLine(crossLineStart, arrowEnd);
            Gizmos.DrawLine(crossLineEnd, arrowEnd);
        }
        //
        public static void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.04f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            DrawArrow(pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }
        public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.04f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);
            DrawArrowEnd(true, pos, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }
        private static void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.05f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Vector3 right = (Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
            Vector3 left = (Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
            Vector3 up = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;
            Vector3 down = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;

            Vector3 arrowTip = pos + (direction * arrowPosition);

            if (gizmos)
            {
                Gizmos.color = color;
                Gizmos.DrawRay(arrowTip, right);
                Gizmos.DrawRay(arrowTip, left);
                Gizmos.DrawRay(arrowTip, up);
                Gizmos.DrawRay(arrowTip, down);
            }
            else
            {
                Debug.DrawRay(arrowTip, right, color);
                Debug.DrawRay(arrowTip, left, color);
                Debug.DrawRay(arrowTip, up, color);
                Debug.DrawRay(arrowTip, down, color);
            }
        }
        public static void DrawInvertArrow(Vector3 pos, Vector3 direction, float tailLennght, Color color, float arrowHeadLength = 0.04f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, -direction);
            DrawArrowEnd(true, pos - direction, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        //
        public static void DrawCrossHair(Vector3 pos, float length)
        {
            DrawCrossHair(pos, length, Gizmos.color);
        }
        public static void DrawCrossHair(Vector3 pos, float length, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, Vector3.up * (length / 2));
            Gizmos.DrawRay(pos, Vector3.left * (length / 2));
            Gizmos.DrawRay(pos, Vector3.forward * (length / 2));
            Gizmos.DrawRay(pos, Vector3.right * (length / 2));
            Gizmos.DrawRay(pos, Vector3.back * (length / 2));
            Gizmos.DrawRay(pos, Vector3.down * (length / 2));
        }

        //
        public static void DrawLine(Vector3 from, Vector3 to)
        {
            DrawLine(from, to, Gizmos.color);
        }
        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
        }

        //
        public static void DrawRay(Vector3 from, Vector3 direction)
        {
            DrawRay(from, direction, Gizmos.color);
        }
        public static void DrawRay(Vector3 from, Vector3 direction, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(from, direction);
        }

        // Рисует круг в плоскости 
        public static void DrawCircle(Vector3 position, float radius, Vector3 direction)
        {
            DrawCircle(position, radius, direction, Gizmos.color);
        }
        public static void DrawCircle(Vector3 position, float radius, Vector3 direction, Color color)
        {
            direction = Vector3.Normalize(direction);
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireDisc(position, direction, radius);
        }

        //
        public static void DrawSphere(Vector3 pos, float length)
        {
            DrawSphere(pos, length, Gizmos.color);
        }
        public static void DrawSphere(Vector3 pos, float length, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(pos, length);
        }
        //
        public static void DrawWireSphere(Vector3 pos, float length)
        {
            DrawWireSphere(pos, length, Gizmos.color);
        }
        public static void DrawWireSphere(Vector3 pos, float length, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(pos, length);
        }

        public static void DrawCube(Vector3 pos, Vector3 size)
        {
            DrawCube(pos, size, Gizmos.color);
        }
        public static void DrawCube(Vector3 pos, Vector3 size, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawCube(pos, size);
        }
        //
        public static void DrawWireCube(Vector3 pos, Vector3 length)
        {
            DrawWireCube(pos, length, Gizmos.color);
        }
        public static void DrawWireCube(Vector3 pos, Vector3 length, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(pos, length);
        }

        //
        public static void DrawTringle(Vector3 pos, Vector3 direction, float length)
        {
            DrawTringle(pos, direction, length, Gizmos.color);
        }
        public static void DrawTringle(Vector3 pos, Vector3 direction, float length, Color color)
        {

            var directionF = Quaternion.LookRotation(direction) * Vector3.forward;
            var directionL = Quaternion.LookRotation(direction) * Vector3.left;
            var directionR = Quaternion.LookRotation(direction) * Vector3.right;

            Vector3 vert1 = pos + (directionF * length);
            Vector3 vert2 = pos + (directionR * length);
            Vector3 vert3 = pos + (directionL * length);

            DrawLine(vert1, vert2, color);
            DrawLine(vert2, vert3, color);
            DrawLine(vert3, vert1, color);
        }

        //
        public static void DrawPoint(Vector3 pos, float length, Color color, PointTipe formPoint)
        {
            switch (formPoint)
            {
                case PointTipe.sphere:
                    Gizmos.DrawWireSphere(pos, length);
                    break;
                case PointTipe.cube:
                    Gizmos.color = color;
                    Gizmos.DrawWireCube(pos, new Vector3(length, length, length));
                    break;
                case PointTipe.crist:
                    DrawCrossHair(pos, length + 0.03f, color);
                    break;
                case PointTipe.all:
                    DrawCrossHair(pos, length + 0.03f, color);
                    Gizmos.DrawWireCube(pos, new Vector3(length, length, length));
                    break;
            }
        }

        // Рисовать мировіе координатнЫе оси как в 3д макс !(Нужно доработать стрелки ояей, буквы осей, и возможность выбора локальных осей)
        public static void DrawGlobalAxis(Vector3 pos, float length, Color color)
        {
            DrawRay(pos, Vector3.up * (length / 2), Color.green);
            DrawRay(pos, Vector3.left * (length / 2), Color.red);
            DrawRay(pos, Vector3.forward * (length / 2), Color.blue);
            DrawRay(pos, Vector3.right * (length / 2), Color.red);
            DrawRay(pos, Vector3.back * (length / 2), Color.blue);
            DrawRay(pos, Vector3.down * (length / 2), Color.green);
            DrawWireCube(pos, new Vector3(length, length, length), Color.black);
        }

        /// <summary>
        /// Draws a flat wire circle (up)
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="segments"></param>
        /// <param name="rotation"></param>
        public static void DrawWireCircle(Vector3 center, float radius, int segments = 20, Quaternion rotation = default(Quaternion))
        {
            DrawWireArc(center, radius, 360, segments, rotation);
        }

        /// <summary>
        /// Draws a wire cylinder face up with a rotation around the center
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        public static void DrawWireCylinder(Vector3 center, float radius, float height, Quaternion rotation = default(Quaternion))
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default(Quaternion)))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            var half = height / 2;

            //draw the 4 outer lines
            Gizmos.DrawLine(Vector3.right * radius - Vector3.up * half, Vector3.right * radius + Vector3.up * half);
            Gizmos.DrawLine(-Vector3.right * radius - Vector3.up * half, -Vector3.right * radius + Vector3.up * half);
            Gizmos.DrawLine(Vector3.forward * radius - Vector3.up * half, Vector3.forward * radius + Vector3.up * half);
            Gizmos.DrawLine(-Vector3.forward * radius - Vector3.up * half, -Vector3.forward * radius + Vector3.up * half);

            //draw the 2 cricles with the center of rotation being the center of the cylinder, not the center of the circle itself
            DrawWireArc(center + Vector3.up * half, radius, 360, 20, Vector3.forward, rotation, center);
            DrawWireArc(center + Vector3.down * half, radius, 360, 20, Vector3.forward, rotation, center);
            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws an arc with a rotation around the center
        /// </summary>
        /// <param name="center">center point</param>
        /// <param name="radius">radiu</param>
        /// <param name="angle">angle in degrees</param>
        /// <param name="segments">number of segments</param>
        /// <param name="rotation">rotation around the center</param>
        public static void DrawWireArc(Vector3 center, float radius, float angle, int segments = 20, Quaternion rotation = default(Quaternion))
        {

            var old = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Vector3 from = Vector3.forward * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
                Gizmos.DrawLine(from, to);
                from = to;
            }

            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws an arc with a rotation around an arbitraty center of rotation
        /// </summary>
        /// <param name="center">the circle's center point</param>
        /// <param name="radius">radius</param>
        /// <param name="angle">angle in degrees</param>
        /// <param name="segments">number of segments</param>
        /// <param name="rotation">rotation around the centerOfRotation</param>
        /// <param name="centerOfRotation">center of rotation</param>
        public static void DrawWireArc(Vector3 center, float radius, float angle, int segments, Vector3 axis, Quaternion rotation, Vector3 centerOfRotation)
        {

            var old = Gizmos.matrix;
            if (rotation.Equals(default(Quaternion)))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(centerOfRotation, rotation, Vector3.one);
            var deltaTranslation = centerOfRotation - center;
            Vector3 from = deltaTranslation + axis * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad)) + deltaTranslation;
                Gizmos.DrawLine(from, to);
                from = to;
            }

            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws an arc with a rotation around an arbitraty center of rotation
        /// </summary>
        /// <param name="matrix">Gizmo matrix applied before drawing</param>
        /// <param name="radius">radius</param>
        /// <param name="angle">angle in degrees</param>
        /// <param name="segments">number of segments</param>
        public static void DrawWireArc(Matrix4x4 matrix, float radius, float angle, int segments, Vector3 axis)
        {
            var old = Gizmos.matrix;
            Gizmos.matrix = matrix;
            Vector3 from = axis * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
                Gizmos.DrawLine(from, to);
                from = to;
            }

            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws a text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public static void DrawString(string text, Vector3 worldPos, Color textColor) 
        {
            DrawString(text, worldPos, textColor, Color.clear);
        }
        public static void DrawString(string text, Vector3 worldPos, Color textColor, Color backColor)
        {
            DrawString(text, worldPos, 600, 1, textColor, backColor);
        }
        public static void DrawString(string text, Vector3 worldPos, int width, int height, Color textColor, Color backColor)
        {
            //#if UNITY_EDITOR

            //var view = UnityEditor.SceneView.currentDrawingSceneView;
            //Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

            //if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            //{
            //    //GUI.color = restoreColor;
            //    Handles.EndGUI();
            //    return;
            //}

            //Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            //GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height - 37, size.x, size.y), text);

            Handles.BeginGUI();
            Handles.color = Color.blue;
            GUIStyle content = new GUIStyle();
            content.normal.textColor = textColor;
            content.normal.background = MakeTex(width, height, backColor);

            Handles.Label(worldPos, text, content);
            Handles.EndGUI();
            //#endif
        }

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        // Рисуем кривую Базье
        public static void DrawBezier(Vector3 startPos, Vector3 endPos, Vector3 startTang, Vector3 endTang)
        {
            //Handles.BeginGUI();
            Handles.DrawBezier(startPos, endPos, startTang, endTang, Color.red, null, 2f);
            //Handles.EndGUI();
        }
        // Рисует пунктирную линию
        public static void DrawDottedLine(Vector3 startPos, Vector3 endPos, Color color, float size = 4) 
        {
            //Handles.BeginGUI();
            Handles.color = color;
            Handles.DrawDottedLine(startPos, endPos, 4);
            //Handles.EndGUI();
        }
        public static void DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            DrawSolidArc(center, normal, from, angle, radius, Gizmos.color);
        }
        public static void DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color color)
        {
            Handles.color = color;
            Handles.DrawSolidArc(center, normal, from, angle, radius);
        }
    }
}

#endif

