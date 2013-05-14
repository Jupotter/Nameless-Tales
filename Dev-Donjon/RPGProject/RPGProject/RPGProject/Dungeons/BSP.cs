using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.Dungeons
{
    class BSP
    {
        int x, y;
        int width, height;

        int position;
        bool horizontal;

        byte level;

        bool leaf;
        BSP father;
        BSP ls, rs;

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Height { get { return height; } }
        public int Width { get { return width; } }
        public int Position { get { return position; } }
        public bool Horizontal { get { return horizontal; } }
        public byte Level { get { return level; } }
        public BSP Left { get { return ls; } }
        public BSP Right { get { return rs; } }
        public BSP Father { get { return father; } }
        public bool Leaf { get { return leaf; } }

        /// <summary>
        /// First, you have to create the root node of the tree. This node encompasses the whole rectangular region.
        /// </summary>
        /// <param name="x">Top Left Corner X</param>
        /// <param name="y">Top Left Corner Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        public BSP(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            width = w;
            height = h;
            leaf = true;
            level = 0;
        }

        BSP(int x, int y, int w, int h, BSP father)
        {
            this.x = x;
            this.y = y;
            width = w;
            height = h;
            leaf = true;
            level = (byte)(father.level + 1);
            this.father = father;
        }

        /// <summary>
        /// Once you have the root node, you can split it into two smaller non-overlapping nodes.
        /// </summary>
        /// <param name="horizontal">If true, the node will be splitted horizontally, else, vertically.</param>
        /// <param name="position">Coordinate of the splitting position.</param>
        public void SplitOnce(bool horizontal, int position)
        {
            if (horizontal)
            {
                ls = new BSP(x, y, position - x, height, this);
                rs = new BSP(position, y, width + x - position, height, this);
            }
            else
            {
                ls = new BSP(x, y, width, position - y, this);
                rs = new BSP(x, position, width, height + y - position, this);
            }
            leaf = false;
            this.horizontal = horizontal;
            this.position = position;
        }

        /// <summary>
        /// You can also recursively split the bsp. At each step, a random orientation (horizontal/vertical) and position are choosen
        /// </summary>
        /// <param name="rand">The random number generator to use.</param>
        /// <param name="nb">Number of recursion levels.</param>
        /// <param name="minHSize">Minimum values of w and h for a node. A node is splitted only if the resulting sub-nodes are bigger than minHSize x minVSize</param>
        /// <param name="minVSize">Minimum values of w and h for a node. A node is splitted only if the resulting sub-nodes are bigger than minHSize x minVSize</param>
        /// <param name="maxHRation">Maximum values of w/h and h/w for a node. If a node does not conform, the splitting orientation is forced to reduce either the w/h or the h/w ratio. Use values near 1.0 to promote square nodes.</param>
        /// <param name="maxVRatio">Maximum values of w/h and h/w for a node. If a node does not conform, the splitting orientation is forced to reduce either the w/h or the h/w ratio. Use values near 1.0 to promote square nodes.</param>
        public void SplitRecursive(Random rand, int nb, int minHSize, int minVSize, float maxHRation, float maxVRatio)
        {
            if (nb <= 0)
                return;
            if ((float)height / width > maxVRatio
                || ((float)width / height < maxHRation
                    && rand.Next(2) == 1))
            {
                int r = rand.Next(height);
                if (r >= minVSize && height - r >= minVSize)
                {
                    SplitOnce(false, r + y);
                    ls.SplitRecursive(rand, nb - 1, minHSize, minVSize, maxHRation, maxVRatio);
                    rs.SplitRecursive(rand, nb - 1, minHSize, minVSize, maxHRation, maxVRatio);
                }
            }
            else
            {
                int r = rand.Next(width);
                if (r >= minHSize && width - r >= minHSize)
                {
                    SplitOnce(true, r + x);
                    ls.SplitRecursive(rand, nb - 1, minHSize, minVSize, maxHRation, maxVRatio);
                    rs.SplitRecursive(rand, nb - 1, minHSize, minVSize, maxHRation, maxVRatio);
                }

                
            }
        }

        public override string ToString()
        {
            if (leaf)
                return String.Format("(Leaf)");

            return String.Format("({0}, {1}: {2}; {3})", position, horizontal, Left.ToString(), Right.ToString());  
        }
    }
}
