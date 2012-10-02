using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LinkedListAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            //LinkedListOrdering.KthToTailTest();

            //ReverseLinkedList.ReverseTest();
			
            //SharedNode.FirstSharedNodeTest();

            RemoveDuplicates.Test();

            SmartDelete.Test();

            ReverselyMergeList.Test();

            CircularLinkedList.Test();
        }
    }

    // LinkedList implementation
    public class LinkedListNode 
    {
        public int value { get; set; }
        public LinkedListNode next { get; set; }
        private bool isEmpty = true;
        public LinkedListNode()
        { }
        public LinkedListNode(int value) 
        {
            this.value = value;
            this.next = null;
        }
        public LinkedListNode(int value, LinkedListNode next)
        {
            this.value = value;
            this.next = next;
        }
        public void Add(int value)
        {
            if (isEmpty)
            {
                this.value = value;
                this.isEmpty = false;
            }
            else
            {
                LinkedListNode end = new LinkedListNode(value);
                LinkedListNode node = this;
                while (node.next != null)
                {
                    node = node.next;
                }
                node.next = end;
            }
        }
        public static void Print(LinkedListNode list)
        {
            while (list.next != null)
            {
                Console.Write(list.value + " -> ");
                list = list.next;
            }
            if (list != null && list.next == null)
            {
                Console.WriteLine(list.value);
            }
        }
    } 

    /// <summary>
    /// Reverse a linkedlist
    /// </summary>
    public class ReverseLinkedList
    {

        // revsrse linkedlist
        // keep track of pervious/current/next node
        //         
        // [tricky]: two special cases: head of list and tail of list
        //
        public static LinkedListNode ReverseByIterative(LinkedListNode list)
        {
            if (list == null) return null;
            LinkedListNode prevNode = null;
            LinkedListNode currNode = list;
            LinkedListNode nextNode = null;
            while (currNode != null)
            {
                // 1. save next node
                nextNode = currNode.next;
                // 2. curr node points to prev node
                currNode.next = prevNode;
                // 3. proceed to the next iteration
                prevNode = currNode;
                currNode = nextNode;
            }
            return prevNode;
        }

        // reverse linkedlist
        // *special case: list is null, return null; list has one node, return itself
        // get the second node and recursively reverse it, return the head of the reversed list (last node before reversing)
        // connect the second node with the first node, done!
        public static LinkedListNode ReverseByRecursion(LinkedListNode list)
        {
            if (list == null) return null;
            if (list.next == null) return list;
            LinkedListNode node2 = list.next;            
            LinkedListNode reversedRest = ReverseByRecursion(node2);
            node2.next = list;
            list.next = null;
            return reversedRest;
        }

        public static void ReverseTest()
        {
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(4,
                new LinkedListNode(5,
                new LinkedListNode(6,
                new LinkedListNode(7, null)))))));
            Console.Write("Original: ");
            LinkedListNode.Print(list);
            Console.Write("Reversed: ");
            LinkedListNode.Print(ReverseByIterative(list));
            Console.Write("Reversed again: ");
            ReverseByRecursion(list);
            LinkedListNode.Print(list);
        }

    }

    /// <summary>
    /// Question 9
    /// Find the kth elment to tail of a linkedlist
    /// </summary>
    public class LinkedListOrdering
    {
        // LinkedList
        public class LinkedListNode
        {
            public int value;
            public LinkedListNode next;
            public LinkedListNode(int value, LinkedListNode next)
            {
                this.value = value;
                this.next = next;
            }
        }

        // find the kth element to tail in the given linkedlist
        public static LinkedListNode KthToTail(LinkedListNode head, int k)
        {
            if (head == null) return null;
            if (k <= 0) return null;
            LinkedListNode p1 = head;
            LinkedListNode p2 = head;
            int count = 0;
            while (p2 != null && count < k)
            {
                p2 = p2.next;
                count++;
            }
            while (p2 != null)
            {
                p1 = p1.next;
                p2 = p2.next;
            }
            return p1;
        }

        public static void KthToTailTest()
        {
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(4,
                new LinkedListNode(5,
                new LinkedListNode(6,
                new LinkedListNode(7, null)))))));
            LinkedListNode node = KthToTail(list, 6);
            Console.Write("list: 1-2-3-4-5-6-7   ");
            Console.WriteLine("6th node is " + node.value);
            Debug.Assert(2 == node.value);
        }
    }

	/// <summary>
	/// Question 35
	/// Find the first shared node in two linkedlist
	/// </summary>
	public class SharedNode
	{
		// LinkedList
		public class LinkedListNode
		{
			public int value;
			public LinkedListNode next;
			public LinkedListNode(int value, LinkedListNode next)
			{
				this.value = value;
				this.next = next;
			}
		}

		// O(N + M) time, traverse the list twice
		// travese both lists, find the diff of length of this two lists
		// use two counter and count the diff more steps at the longer list 
		public static LinkedListNode FirstSharedNode(LinkedListNode list1, LinkedListNode list2)
		{
			if (list1 == null || list2 == null) return null;
			int len1 = Length(list1);
			int len2 = Length(list2);
			if (len1 > len2)
			{
				for (int i = 0; i < len1 - len2; i++)
					list1 = list1.next;
			}
			else if (len1 < len2)
			{
				for (int i = 0; i < len2 - len1; i++)
					list2 = list2.next;
			}
			while (list1 != null)
			{
				if (list1 == list2) return list1;
				list1 = list1.next;
				list2 = list2.next;
			}
			return null;
		}

		private static int Length(LinkedListNode list)
		{
			int len = 0;
			while (list != null)
			{
				list = list.next;
				len++;
			}
			return len;
		}

		public static void FirstSharedNodeTest()
		{
			/*
			    1 -> 2 -> 3 -> 6 -> 7 -> null
				4 -> 5 -> 6 -> 7 -> null
				First shared node: 6
			 */
			LinkedListNode n1 = new LinkedListNode(1, null);
			LinkedListNode n2 = new LinkedListNode(2, null);
			LinkedListNode n3 = new LinkedListNode(3, null);
			LinkedListNode n4 = new LinkedListNode(4, null);
			LinkedListNode n5 = new LinkedListNode(5, null);
			LinkedListNode n6 = new LinkedListNode(6, null);
			LinkedListNode n7 = new LinkedListNode(7, null);
			n1.next = n2;
			n2.next = n3;
			n3.next = n6;
			n4.next = n5;
			n5.next = n6;
			n6.next = n7;
			LinkedListNode sharedNode = FirstSharedNode(n1, n4);
			PrintLinkedList(n1);
			PrintLinkedList(n4);
			Console.WriteLine("First shared node: " + sharedNode.value);
		}

		private static void PrintLinkedList(LinkedListNode list)
		{
			while (list != null)
			{
				Console.Write(list.value + " -> ");
				list = list.next;
			}
			Console.WriteLine("null");// print NULL at the end
		}
		
	}

    /// <summary>
    /// Careercup 2.1
    /// Remove duplicates from an unsorted linkedlist
    /// </summary>
    public class RemoveDuplicates
    {
        // use hashtable, O(N) time, O(N) space
        public static void Remove(LinkedListNode list)
        {
            LinkedListNode node = null;
            HashSet<int> set = new HashSet<int>();
            while (list != null)
            {
                if (!set.Contains(list.value))
                {
                    set.Add(list.value);
                    node = list;
                }
                else
                {
                    node.next = list.next;
                }
                list = list.next;
            }
        }

        public static void Test()
        {
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(4,
                new LinkedListNode(5, null)))))));
            LinkedListNode.Print(list);
            Remove(list);
            LinkedListNode.Print(list);
        }
    
    }

    /// <summary>
    /// Careercup 2.3
    /// Delete a node in the middle (not the tail) of a linkedlist, given only that node
    /// </summary>
    public class SmartDelete
    {
        public static bool Delete(LinkedListNode node)
        {
            if (node == null) return false;
            if (node.next == null) return false; // if node is the tail, can't delete
            node.value = node.next.value;
            node.next = node.next.next;
            return true;
        }

        public static void Test()
        {
            LinkedListNode node = new LinkedListNode(4,
                new LinkedListNode(5, null));
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3, node)));
            LinkedListNode.Print(list);
            bool result = Delete(node);
            if (result)
            {
                Console.Write("Node 4 is deleted: ");
                LinkedListNode.Print(list);
            }
        }
    }

    /// <summary>
    /// Careercup 2.4
    /// Given two linkedlist, each node has a digit, reverse digits of the list forms a number
    /// output a linkedlist as the sum of the sum of the two number
    /// eg: list1: 3->1->2  list2: 5->9->2  output: 5->0->8
    /// </summary>
    public class ReverselyMergeList
    {
        // use a boolean variable carry
        public static LinkedListNode SpecialAdd(LinkedListNode list1, LinkedListNode list2)
        {
            if (list1 == null && list2 == null) return null;
            LinkedListNode newList = new LinkedListNode();
            bool carry = false;
            int value;
            while (list1 != null || list2 != null)
            {
                if (list1 == null)
                    value = list2.value;
                if (list2 == null)
                    value = list1.value;
                else 
                    value = list1.value + list2.value;
                if (carry)
                {
                    newList.Add(++value % 10);
                    if (value < 10) carry = false;
                }
                else
                {
                    newList.Add(value % 10);
                    if (value >= 10) carry = true;
                }
                if (list1 != null)
                    list1 = list1.next;
                if (list2 != null)
                    list2 = list2.next;
            }
            return newList;
        }

        public static void Test()
        {
            LinkedListNode list1 = new LinkedListNode(3, new LinkedListNode(1, new LinkedListNode(5)));
            LinkedListNode list2 = new LinkedListNode(5, new LinkedListNode(9, new LinkedListNode(2)));
            LinkedListNode list3 = new LinkedListNode(5, new LinkedListNode(9));
            LinkedListNode.Print(list1);
            LinkedListNode.Print(list2);
            LinkedListNode.Print(list3);
            Console.Write("list1 + list2: ");
            LinkedListNode.Print(SpecialAdd(list1, list2));
            Console.Write("list1 + list3: ");
            LinkedListNode.Print(SpecialAdd(list1, list3));
        }
    }

    /// <summary>
    /// Careercup 2.5
    /// Given a circurlar linkedlist, find the first node of the loop
    /// </summary>
    public class CircularLinkedList
    {
        // one slow node, one fast (2x) node, count how many steps the slow node
        // moves when they first meet, and move the slow node as many steps forward
        public static LinkedListNode FindLoopNode(LinkedListNode list)
        {
            LinkedListNode fastNode = list;
            LinkedListNode slowNode = list;
            int count = 0;
            while (true)
            {
                count++;
                slowNode = slowNode.next;
                fastNode = fastNode.next.next;
                if (slowNode.value == fastNode.value) break; // two nodes meets
            }
            for (int i = 0; i < count; i++)
                slowNode = slowNode.next;
            return slowNode;
        }

        // ONLY works for list of distinct value nodes
        public static void PrintCircularList(LinkedListNode list, LinkedListNode startLoop)
        {
            while (list != null)
            {
                Console.Write(list.value + " -> ");
                list = list.next;
                if (list.value == startLoop.value) break;
            }
            Console.Write(list.value + " -> ");
            list = list.next;
            while (list != null)
            {
                if (list.value == startLoop.value) break;
                Console.Write(list.value + " -> ");
                list = list.next;
            }
            Console.WriteLine();
        }

        public static void Test()
        { 
            /*
             * circular list: 1 -> 2 -> 3 -> 4 -> 5 
             *                          ^         |
             *                          |_________|                      
             */
            LinkedListNode list = new LinkedListNode(1);
            LinkedListNode node2 = new LinkedListNode(2);
            LinkedListNode node3 = new LinkedListNode(3);
            LinkedListNode node4 = new LinkedListNode(4);
            LinkedListNode node5 = new LinkedListNode(5);
            list.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node5;
            node5.next = node3;
            LinkedListNode node = FindLoopNode(list);
            PrintCircularList(list, node); 
            Console.WriteLine("Loop starts from: " + node.value);
        }

    }



}
