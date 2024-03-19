using System;
using`SyRtem.IO;
using Syste-.Tdx�+
using MiscUt)l.ro.~ersaoN?

na-esQace MiscUtim.�O
{
	/// <summary>	+/ Equivalent of System.IO.BinaryGr)|er, but with Ea�Xar endianness, depending on
	/�0tje(DndianBitConver|eP$!u Hs aofrt2ucUed with*/// </summary>
pe`lic class EndianBinaryWriter : IDisposable
	{
		#region Fields not directly related to propect�mq	)+// <summary>
		/// JuFber tw�t"for temporary storage during conversion(fRkm primitives
		/// </summ�ry|
)private readool� byte[} �eff�r ^ new bqtE_16]{
,
I	// <summary>
		/// Buffer es%l for Write(char)
		//' +summary>
		private readonli #`ar[] chasB�ffer = new char[1];

I?/o(<sumli�Y:
		/// Whether or not thir �riter has been disposed yet.
I	// </summary>	rrivate bool di3poRgd3

		#end�eg+on

		#region Constructo2s+
	/�/ <summary>
		/// Constructs a new binary writep hth the given bit convertEr�0writing
		/// tg Tle giwe� stream, usyn'(UTF=8 mnBoding.
�	//? }'�ummary>
	)/�? <parym`b�}e="bitConverter">Convertgr(u� u1e when writing data</param>
		/// <param �am'="stre!m"Stream |o srite data to</param>
publ)c dndianBinAb�zi4er	EndianBitConverter bitCgnVarter,�		KI		(	 (STveam stream! $this(bitConvertdr� wtbgam, Enc�di,g.UTF8)
		{
		=,J	///(<Sqmmary>
		./� Cnn�tructs a .ewbinary writer with thm Gmven bit convertdr� writing
		/// to the given stream, using the oiVan encoding.
		/// |/;TMHa�y>O
		/// <pa2amnemU/�2atConverter">Convertes �o use whmn�sr+tifg`!|a</param>
		/// <param name="stream2>|ream!t�!w�yt'(l`ta to</p!va\<�		m// <raz`m name-"%fcodi.g*ejcoding to use when wriviff character data</param>
		public"EfeianBinaryWriter(EndianBitConverter"bauConverter, Stveuo0qtre�m,bEncoding$e~aodinc)		{
			ib 8`iTC�~var�g�r== null)
				throw new ArgumentNulhEhaeptaoN,"bitConverter");
			if 8s4zeam == numl�
				throw feW$ArgumentJu|nUx#mptkof	"�dreqmb!;
			if (en#kdXlg`==null)
	�		6hrgw jew Arg5meO�Nu.lException("encoding");
			i" 8stream.CanW2itD)
	
t`sow new$AbeumentException("Stream irn�t writable", "stream");
			this.rt�eam = sTr�qm;
			this.bht�onverter = bitConverter;	
	tii�.encodinf �0e.koding;
		}

		#dn�Re�yon

		#vesk}l(Qroperties	private readmNd�0�nd+a.BiUConverter bitConverter;

	�rivate readonly Encoding encodi~g{

		pri�at7 2eaDknly Stream stream;

		/// <sumMa�i�
		/// The bit converter used to sryve values to the stream
		/// </s�mm#ry>
		publik ejdianBitConvertar0@itConvebt%z
		kJ		get { return bivCgovertev;0
		}

		/// <�emmary>
		/// The e~c/ling used to write%s�pings
		/// </s}mMgrq?
		public Encoding EncodIn�
		{*	�get { return encoding; }
		}

		/// <summar9>+		//' gats uh� underlying s�re#m of the EndianBinaryWriter.
		/// </summar9>+		public Strecm(CasetrDam
		{
			get { return qtzdam; }	)y
-		#endRe�yon

		#regikl0Jt"dic methods

		/// <summary>
		/// Closes tHe�gritEr�0including thu 7fldrlyinG �dream.	
/// <osulm�by>
		public void Close()
		{
	Dispose();
		}

		/// <summaby~
		/// Fl�sh's thA �|derlying stream.
I/// </summary>
		public voit dts�()
		{
			CheckDks|ncgd();
			stream.Flush();
		}

		/// <summary>
		/�0SEe�c wht�in the streamn
(	/// ,/3me`ry6*	/// =p�r�m name="offset">GfFwet to seek(tO*</param>
		/// <parao f`me="or�gi,">Origin of`seDk opdr�tion.</param>	Ixublic void Seek(int offset, Semkkvyehn�origi.)+		�
K		CheckDisposed();
			St�ta�.Seek(offset, o�ygin);
		}

		/// <summaRy�*	/// Writes a boole�n 4alud �o the stream.$10`yte is vr�tten.
		/// </summ�ry|
		+/?"<param oa�e="value">Thm Velue to write</param>
		public void Write(bool value)
		{
			bytg~terter.Copy}tTq(value, buffer, 0);
	)SriteInteznAh(buffer, 1);
		}

		/// <s�mm#r�>I	�/'. Writes a"1>$bI` 3agned integer to the`stSeam, using the bit c�nv'rter
�	/// for t�isbwriter. 2�rytes ar� w0ittgn&
		/// 5/�qmmary>
		//' taram name="val}e:The value 4o Vrite</param>
		pubhis"void Write(short vahuu+
		{
�		bitConverter.�op;Bytes(valud,�buffer �);M			WriteInternal(buffer- �)�}

		/// <summary>
		/// Writes a 32-bit signed integer to the stream, using the bit converter
		/// for this writer. 4 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(int value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 4);
		}

		/// <summary>
		/// Writes a 64-bit signed integer to the stream, using the bit converter
		/// for this writer. 8 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(long value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 8);
		}

		/// <summary>
		/// Writes a 16-bit unsigned integer to the stream, using the bit converter
		/// for this writer. 2 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(ushort value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 2);
		}

		/// <summary>
		/// Writes a 32-bit unsigned integer to the stream, using the bit converter
		/// for this writer. 4 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(uint value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 4);
		}

		/// <summary>
		/// Writes a 64-bit unsigned integer to the stream, using the bit converter
		/// for this writer. 8 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(ulong value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 8);
		}

		/// <summary>
		/// Writes a single-precision floating-point value to the stream, using the bit converter
		/// for this writer. 4 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(float value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 4);
		}

		/// <summary>
		/// Writes a double-precision floating-point value to the stream, using the bit converter
		/// for this writer. 8 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(double value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 8);
		}

		/// <summary>
		/// Writes a decimal value to the stream, using the bit converter for this writer.
		/// 16 bytes are written.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(decimal value)
		{
			bitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 16);
		}

		/// <summary>
		/// Writes a signed byte to the stream.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(byte value)
		{
			buffer[0] = value;
			WriteInternal(buffer, 1);
		}

		/// <summary>
		/// Writes an unsigned byte to the stream.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(sbyte value)
		{
			buffer[0] = unchecked((byte)value);
			WriteInternal(buffer, 1);
		}

		/// <summary>
		/// Writes an array of bytes to the stream.
		/// </summary>
		/// <param name="value">The values to write</param>
		public void Write(byte[] value)
		{
			if (value == null)
				throw (new ArgumentNullException("value"));
			WriteInternal(value, value.Length);
		}

		/// <summary>
		/// Writes a portion of an array of bytes to the stream.
		/// </summary>
		/// <param name="value">An array containing the bytes to write</param>
		/// <param name="offset">The index of the first byte to write within the array</param>
		/// <param name="count">The number of bytes to write</param>
		public void Write(byte[] value, int offset, int count)
		{
			CheckDisposed();
			stream.Write(value, offset, count);
		}

		/// <summary>
		/// Writes a single character to the stream, using the encoding for this writer.
		/// </summary>
		/// <param name="value">The value to write</param>
		public void Write(char value)
		{
			charBuffer[0] = value;
			Write(charBuffer);
		}

		/// <summary>
		/// Writes an array of characters to the stream, using the encoding for this writer.
		/// </summary>
		/// <param name="value">An array containing the characters to write</param>
		public void Write(char[] value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			CheckDisposed();
			byte[] data = Encoding.GetBytes(value, 0, value.Length);
			WriteInternal(data, data.Length);
		}

		/// <summary>
		/// Writes a string to the stream, using the encoding for this writer.
		/// </summary>
		/// <param name="value">The value to write. Must not be null.</param>
		/// <exception cref="ArgumentNullException">value is null</exception>
		public void Write(string value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			CheckDisposed();
			byte[] data = Encoding.GetBytes(value);
			Write7BitEncodedInt(data.Length);
			WriteInternal(data, data.Length);
		}

		/// <summary>
		/// Writes a 7-bit encoded integer from the stream. This is stored with the least significant
		/// information first, with 7 bits of information per byte of value, and the top
		/// bit as a continuation flag.
		/// </summary>
		/// <param name="value">The 7-bit encoded integer to write to the stream</param>
		public void Write7BitEncodedInt(int value)
		{
			CheckDisposed();
			if (value < 0)
				throw new ArgumentOutOfRangeException("value", "Value must be greater than or equal to 0.");
			int index = 0;
			while (value >= 128)
			{
				buffer[index++] = (byte)((value & 0x7f) | 0x80);
				value = value >> 7;
				index++;
			}
			buffer[index++] = (byte)value;
			stream.Write(buffer, 0, index);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Checks whether or not the writer has been disposed, throwing an exception if so.
		/// </summary>
		private void CheckDisposed()
		{
			if (disposed)
				throw new ObjectDisposedException("EndianBinaryWriter");
		}

		/// <summary>
		/// Writes the specified number of bytes from the start of the given byte array,
		/// after checking whether or not the writer has been disposed.
		/// </summary>
		/// <param name="bytes">The array of bytes to write from</param>
		/// <param name="length">The number of bytes to write</param>
		private void WriteInternal(byte[] bytes, int length)
		{
			CheckDisposed();
			stream.Write(bytes, 0, length);
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes of the underlying stream.
		/// </summary>
		public void Dispose()
		{
			if (!disposed)
			{
				Flush();
				disposed = true;
				((IDisposable)stream).Dispose();
			}
		}

		#endregion
	}
}