/* Stream Reader Class for PKM.Saves
 * By JamPaul97
 * Version : 0.0.1
 * Attribution-NonCommercial-NoDerivs 3.0 Unported (CC BY-NC-ND 3.0)
 * https://github.com/JamPaul97
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PKM.Saves.Tools
{
	/// <summary>
	/// Stream Reader class for easier byte read.
	/// </summary>
    public class StreamReader
    {
        private byte[] data;
        private string filename;
		private ulong index = 0;
		/// <summary>
		/// The whole byte array contained all the data
		/// </summary>
        public byte[] Data { get { return data; } }
		/// <summary>
		/// The filename that the data was read from.
		/// </summary>
        public string File { get { return this.filename; } }
		/// <summary>
		/// The current index of the reading byte.
		/// </summary>
		public ulong CurrentIndex { get { return this.index; } }
		
        public StreamReader(string filename)
		{
            if (!System.IO.File.Exists(filename))
                throw new FileNotFoundException();
			else
			{
				try
				{
					this.data = System.IO.File.ReadAllBytes(filename);
					this.filename = filename;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}
		public StreamReader(byte[] data)
		{
			this.data = data;
			this.filename = string.Empty;
		}
		/// <summary>
		/// True if can read one byte from the stream.
		/// </summary>
		/// <returns></returns>
		public bool CanRead()
		{
			return this.index <= (ulong)this.data.Length;
		}
		/// <summary>
		/// True if can read x(number) bytes from stream
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public bool CanRead(uint number)
		{
			return (this.index + number <= (ulong)this.data.Length);
		}
		/// <summary>
		/// Peeks at the next byte of the stream without consumming it.
		/// </summary>
		/// <returns></returns>
		public byte Peek()
		{
			if (!this.CanRead())
				throw new IndexOutOfRangeException();
			return this.data[this.index];
		}
		/// <summary>
		/// Read a single byte from the stream
		/// </summary>
		/// <returns></returns>
		public byte Read()
		{
			if (!this.CanRead())
				throw new IndexOutOfRangeException();
			byte re = this.data[this.index];
			this.index++;
			return re;
		}
		/// <summary>
		/// Read a ushort from the stream
		/// </summary>
		/// <returns></returns>
		public ushort ReadUShort(bool BigEndian = true)
		{
			if (!this.CanRead(2))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToUInt16(arr, 0);
		}
		/// <summary>
		/// Read a short from the stream.
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public short ReadShort(bool BigEndian = true)
		{
			if (!this.CanRead(2))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToInt16(arr, 0);
		}
		/// <summary>
		/// Reads a Uint from the stream.
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public uint ReadUint(bool BigEndian = true)
		{
			if (!this.CanRead(4))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read(), this.Read(),this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToUInt32(arr, 0);
		}
		/// <summary>
		/// Returns an Int from the stream.
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public int ReadInt(bool BigEndian = true)
		{
			if (!this.CanRead(4))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read(), this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToInt32(arr, 0);
		}
		/// <summary>
		/// Reads a Ulong from the stream.
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public ulong ReadUlong(bool BigEndian = true)
		{
			if (!this.CanRead(8))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToUInt64(arr, 0);
		}
		/// <summary>
		/// Reads a long from the stream.
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public long Readlong(bool BigEndian = true)
		{
			if (!this.CanRead(8))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToInt64(arr, 0);
		}
		/// <summary>
		/// Read a float from the stream
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public float ReadFloat(bool BigEndian = true)
		{
			if (!this.CanRead(4))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read(), this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToSingle(arr, 0);
		}
		/// <summary>
		/// Reads a double from the stream
		/// </summary>
		/// <param name="BigEndian"></param>
		/// <returns></returns>
		public double ReadDouble(bool BigEndian = true)
		{
			if (!this.CanRead(8))
				throw new IndexOutOfRangeException();
			byte[] arr = new byte[] { this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read(), this.Read() };
			if (BigEndian)
				Array.Reverse(arr);
			return BitConverter.ToDouble(arr, 0);
		}
		/// <summary>
		/// Reads a block of byte from the stream
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public byte[] ReadBlock(int size)
		{
			if (!this.CanRead((uint)size))
				throw new IndexOutOfRangeException();
			List<byte> arr = new List<byte>();
			for (int i = 0; i < size; i++)
				arr.Add(this.Read());
			return arr.ToArray();
		}
		/// <summary>
		/// Reads a UTF string from the stream
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>

		public string ReadString(int size)
		{
			if (!this.CanRead((uint)size))
				throw new IndexOutOfRangeException();
			var bytes = this.ReadBlock(size);
			return Encoding.UTF8.GetString(bytes);
		}

	}
}
