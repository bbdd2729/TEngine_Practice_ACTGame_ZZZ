/*           INFINITY CODE          */
/*     https://infinity-code.com    */

namespace InfinityCode.UltimateEditorEnhancer
{
    public class SortedLinkedList<T>
    {
        public int capacity;
        private Item tail;
        
        public int count { get; private set; }
        public float maxValue { get; private set; }

        public SortedLinkedList(int capacity)
        {
            this.capacity = capacity;
        }
        
        public void Add(T element, float value)
        {
            if (count == capacity)
            {
                Item t = tail;
                tail = t.prev;
                t.prev = null;
                count--;
            }

            AddValue(element, value);

            maxValue = tail.value;
        }

        private void AddValue(T element, float value)
        {
            Item item = new Item { element =  element, value = value };

            if (count == 0)
            {
                tail = item;
                count = 1;
                return;
            }
            
            if (value >= tail.value)
            {
                item.prev = tail;
                tail = item;
                count++;
                return;
            }
            
            Item current = tail;
            while (current.prev != null && current.prev.value > value)
            {
                current = current.prev;
            }
            
            item.prev = current.prev;
            current.prev = item;
            count++;
        }
        
        public T[] ToArray()
        {
            T[] array = new T[count];
            Item current = tail;
            for (int i = count - 1; i >= 0; i--)
            {
                array[i] = current.element;
                current = current.prev;
            }
            return array;
        }

        private class Item
        {
            public T element;
            public float value;
            public Item prev;
        }
    }
}