using System;

class HeapSort
{
    public static void Heapify(int[] arr, int n, int i)
    {
        int largest = i; // Initialize largest as root
        int left = 2 * i + 1; // Left child
        int right = 2 * i + 2; // Right child

        // If left child is larger than root
        if (left < n && arr[left] > arr[largest])
            largest = left;

        // If right child is larger than largest so far
        if (right < n && arr[right] > arr[largest])
            largest = right;

        // If largest is not root
        if (largest != i)
        {
            // Swap root with largest
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;

            // Recursively heapify the affected subtree
            Heapify(arr, n, largest);
        }
    }

    public static void BuildHeap(int[] arr, int n)
    {
        // Start from the last non-leaf node and heapify each one
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, n, i);
        }
    }

    public static void HeapSortAlgorithm(int[] arr)
    {
        int n = arr.Length;

        // Step 1: Build a max-heap
        BuildHeap(arr, n);

        // Step 2: Extract elements one by one
        for (int i = n - 1; i > 0; i--)
        {
            // Move current root to end
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // Call heapify on the reduced heap
            Heapify(arr, i, 0);
        }
    }

    public static void Main()
    {
        int[] arr = { 12, 11, 13, 5, 6, 7 };

        Console.WriteLine("Original array:");
        Console.WriteLine(string.Join(" ", arr));

        HeapSortAlgorithm(arr);

        Console.WriteLine("Sorted array:");
        Console.WriteLine(string.Join(" ", arr));
    }
}
