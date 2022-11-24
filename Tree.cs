namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;
        private T value;
        private Tree<T> parent;
        public Tree(T value)
        {
            this.value = value;
            this.children = new List<Tree<T>>();

        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {

                child.parent = this;
                this.children.Add(child);

            }

        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentNode = this.FindNodeWithBfs(parentKey);

            if (parentNode == null)
            {

                throw new ArgumentNullException("No way!");
            }
            parentNode.children.Add(child);
            child.parent = parentNode;
        }

        private Tree<T> FindNodeWithBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);


            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();


                if (subTree.value.Equals(parentKey))
                {
                    return subTree;
                }

                foreach (var child in subTree.children)
                {

                    queue.Enqueue(child);
                }

            }

            return null;
        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            var result = new List<T>();

            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.value);

                foreach (var child in subTree.children)
                {

                    queue.Enqueue(child);
                }

            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {

            var order = new List<T>();
            this.DfsWithRecursion(this, order);

            return order;
        }
        private void DfsWithRecursion(Tree<T> node, ICollection<T> result)
        {
            foreach (var child in node.children)
            {
                this.DfsWithRecursion(child, result);
            }
            result.Add(node.value);
        }

        private IEnumerable<T> DfsWithStack()
        {
            var result = new Stack<T>();
            var stack = new Stack<Tree<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {

                var node = stack.Pop();
                foreach (var child in node.children)
                {

                    stack.Push(child);
                }
                result.Push(node.value);
            }
            return result;

        }

        public void RemoveNode(T nodeKey)
        {
            var toBeDeletedNode = this.FindNodeWithBfs(nodeKey);

            if (toBeDeletedNode == null)
            {

                throw new ArgumentNullException("Dont have any node");
            }

            var parentNode = toBeDeletedNode.parent;
           
            if (parentNode == null)
            {
                throw new ArgumentException("Dont have any rout");

            }

            parentNode.children.Remove(toBeDeletedNode);
                
           

        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindNodeWithBfs(firstKey);
            var secondNode = this.FindNodeWithBfs(secondKey);

            if (firstNode == null || secondNode == null)
            {
                throw new ArgumentNullException("Null input");
            }

            var firstParent = firstNode.parent;
            var secondParent= secondNode.parent;

            if (firstParent == null || secondParent == null)
            {
                throw new ArgumentException("Dont have any rout");

            }

            var indexOfFirstChild = firstParent.children.IndexOf(firstNode);
            var indexOfSecondChild = secondParent.children.IndexOf(secondNode);

            firstParent.children[indexOfFirstChild] = secondNode;
            secondNode.parent = firstParent;
           
            secondParent.children[indexOfSecondChild] = firstNode;
            firstNode.parent = secondParent;

        }


    }
}
