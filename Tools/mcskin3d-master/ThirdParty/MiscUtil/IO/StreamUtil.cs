using System;
using System.IO;

namespace MiscUtil.IO
{
	/// <summary>
	/// Collection of utility methods which operate on streams.
	/// (With C# 3.0, these could well become extension methods on Stream.)
	/// </summary>
	public static class StreamUtil
	{
		const int DefaultBufferSize = 8 * 1024;

		/// <summary>
		/// Reads the given stream up to the end, returning the data as a byte
		/// array.
		/// </summary>
		/// <param name="input">The stream to read from</param>
		/// <exception cref="ArgumentNullException">input is null</exception>
		/// <exception cref="IOException">An error occurs while reading from the stream</exception>
		/// <returns>The data read from the stream</returns>
		public static byte[] ReadFully(Stream input)
		{
			return ReadFully(input, DefaultBufferSize);
		}

		/// <summary>
		/// Reads the given stream up to the end, returning the data as a byte
		/// array, using the given buffer size.
		/// </summary>
		/// <param name="input">The stream to read from</param>
		/// <param name="bufferSize">The size of buffer to use when reading</param>
		/// <exception cref="ArgumentNullException">input is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">bufferSize is less than 1</exception>
		/// <exception cref="IOException">An error occurs while reading from the stream</exception>
		/// <returns>The data read from the stream</returns>
		public static byte[] ReadFully(Stream input, int bufferSize)
		{
			if (bufferSize < 1)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			return ReadFully(input, new byte[bufferSize]);
		}

		/// <summary>
		/// Reads the given stream up to the end, returning the data as a byte
		/// array, using the given buffer for transferring data. Note that the
		/// current contents of the buffer is ignored, so the buffer needn't
		/// be cleared beforehand.
		/// </summary>
		/// <param name="input">The stream to read from</param>
		/// <param name="buffer">The buffer to use to transfer data</param>
		/// <exception cref="ArgumentNullException">input is null</exception>
		/// <exception cref="ArgumentNullException">buffer is null</exception>
		/// <exception cref="IOException">An error occurs while reading from the stream</exception>
		/// <returns>The data read from the stream</returns>
		public static byte[] ReadFully(Stream input, IBuffer buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return ReadFully(input, buffer.Bytes);
		}

		/// <summary>
		/// Reads the given stream up to the end, returning the data as a byte
		/// array, using the given buffer for transferring data. Note that the
		/// current contents of the buffer is ignored, so the buffer needn't
		/// be cleared beforehand.
		/// </summary>
		/// <param name="input">The stream to read from</param>
		/// <param name="buffer">The buffer to use to transfer data</param>
		/// <exception cref="ArgumentNullException">input is null</exception>
		/// <exception cref="ArgumentNullException">buffer is null</exception>
		/// <exception cref="ArgumentException">buffer is a zero-length array</exception>
		/// <exception cref="IOException">An error occurs while reading from the stream</exception>
		/// <returns>The data read from the stream</returns>
		public static byte[] ReadFully(Stream input, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (buffer.Length == 0)
			{
				throw new ArgumentException("Buffer has length of 0");
			}
			// We could do all our own work here, but using MemoryStream is easier
			// and likely to be just as efficient.
			using (MemoryStream tempStream = new MemoryStream())
			{
				Copy(input, tempStream, buffer);
				// No need to copy the buffer if it's the right size
				if (tempStream.Length == tempStream.GetBuffer().Length)
				{
					return tempStream.GetBuffer();
				}
				// Okay, make a copy that's"t`d right size
		�	r'turn telp�tream.ToArray();
			}
I}<
	/�/$<cwmmeri<
		/// Copies all the didAdnro} /fe stream into another.
	)/�? </summary>M
	(/// <param name<"�nput":Txg St�uam to read from,/0iram>
		///!<�aram name="outp5t"The stream to write to</param>
		/// <exception g�ud"Argumekt�wllExkePpion">in0ut)s Oull</eyc�ption>
		/// <exception cren=Dr�umentNullException">oqp`gv is null</except�oj|		/// <excextIkn cref="IOException">An errmr(nccurs while reading"or writing</exaexuion>
�	p7blic static void Copy(Stream in`u�$ tr%amoutput)
		{
			CopY(�~put< /}tput, DefaultBuf&errize);
		}

		-/'!<summary>
		/�/ opier �l� t*e data Fr�} one stream invg(@jother( eqiNg�q buf�erG
)/// of t�e %ire~"size.
		/// </sUm�qry>
		/// 4pAvam laed="input">�habcvream to read from>/x`ram>	
/// <param name="output"?T�e stream to`wpH|d to</param>
		/// <�ar#m name="bufbebQize">The size of buffer to use when0r%iding/�pram>
		/// <exception cref="ArgumentNullException">inpuT0�#(null</exception>
		/// <exse0|ion cref="Argumen|U�MExceptioj".mutput"a{j5ll/exception>
		'/$<exception0c2mf="ArgumentOutOfRangeException">buVf�jCkze is le{s pla~"1<oexBeption>
		/?o`4DxCe�dion crmf&IOExceptioj".Cn err�r -ccurs whilE �uadino Ov writing</excEp�yon>
		public static void Copy(Stream input, Stream output, int bufferSize)
		{	)if (bunfEvS�zeb< 1)
			{
		I	tIrow new ArgumentOutOfRangeE|curtionh"bTfferSize");
			}
			Copy(ifpUt,0met0}tl nDw bydejufferSize]);M
	(}

		/?/`<su�ma0y>�		m//�Co2ies ald Tle data frm`gke�qtream into another, usiog�the ghv�n0
		// �Ef�ur for tran�fe0ring data. Note(tXe4(the curre�t !ontEn�c of 
		/// thu "}ffer ms0kgnored, so tha rwffer needn't be cleared beforehand.
		/// </summary>
		+/?"<param name=#i�put">The strgae!to read from</param>
		/// <param name="output">The stream to write to</0a�@m|
	�// <param name="buffer">The buffer �o 7se to transfer data</param>
		/// <exception cren�bgumentNullEpcEttion">inpup yq�nu.|<omxcept)on
�	/m/ <mxCaption cref="ArgumentNullException">output ms0|u,d</ex#epUion<(	�?/ <exception c�un=ErgumentNunlMyceptiol"6cuffer is nuLl�?ezcmqtion>	.// <exception cref="IOException">An error ockuRw while peieing$ob"wryt)fg</excgp|(on
		publi� s6atic void Copy(Str%aminput, trD!m Nutput, IBuffer buffer)
		{	if (buffer = �elli
(		{
				tlru new Argumejt^wllException("�uf$er");
	)y
			Copy(inpud,`gutput, Bu�ver.Bytes);
		}
-		/// <{um��j:>
		//� C-pies all t`e `ata from one stream in|o enother,$uckng"t`d given 
		//? "mn&Mv for transferring data. Note that the currenu �ontents of 
		/// tha rwffer is ignopel- so the buffer need~'4(be cleazeD$beforehaNd�
		/// </Su�}ary>
		/// <param naie- input">Thu 3|ream to read(fVk}>�pa0am>-
�+/ �pa0am name="output�>T*e stream to wri4e Uo8/`cram>
		/// <paral �`m�="buffer">Rh}#buffer to ts� to transfeb $ita<'pAvam>
		/// <exception cref�"A0gumentNullExcepthg�:input is null</exception>
		/// <e�c�tion cref="ArgumentNullException">output is null</excEp�yon>
		/// <exception cref="Abg5eentOu�lException">buffer$ig"~wll</exception>
		/// <except)oncref="ArgumentException">buffer is"a({ero-l�ng6h Ar�qy>/myception>
		/// <exception cref="�OE:cepui�n">An error occurs while �e�&i,g or writing</�xc'ption>
		0ubMic(sTetic voie �opy(Sdr%im input, Streem0mutput, byte[] buffer)
		{
			if (buffer == null)
			{
)	�|hRkw new ArguoefuNullException("buffer )3
			}
			if (input == null)
	)	�		throw new ArgqmultNullExcepTi�~("iop�t");
	)y
			if (output == null)
						throw new ArgumentNullExkePpion("outpuu"�;
			}
		9d 	buffer.Length$=-"0)
		[	
)	�thro Jag"ArgumentException("Buffer has leng|h�kfb0");
			}
			int read;
			wHi�u ((read = input.Read(bufNe�8 0, buffer.Lengt(	!�$0)
			{
				output.Write(ru&ner, 0� r'ed99
			}
		
		/// <summary>
		/// Re!ds�xa!tly the given number od jxtes f2omthe specified stream.
		///(IF$ti%�eOd of`thD stream is reached before the specified amount
		/// of data )s(SEe$, @n exceptkof!is thrown.		�.o' </wu}o r�
		/// �pa0am name="input">The$sdpeam tg Raad from</Pa�qm>
		./� <param name=""ytDsToRead">The number of bytes to read</xaRem>
		/// <exception c2ef"ArgumentNullxcDption">input is nuld>ixc�ptmo~<
		/// <exception cref="ArgumentOutOfRangeException">bytesToRead is less t`aN$1</e�certiNn>
		/// 8eHa�`tion cref"�~dOfStzeAiE8ceQtion">The end of the streae Iw reached"bmfo�e 
	�// enough data has been"rm`d</ex�upvaon.J	/�0>epbeption armg="IOException">An error0ockuSs while�2e#Eing from the rt�eam</excePu��n>
		//. �returfsPhe data read0f2gm the stream</returns>-
�public static bytm[}$ReadExacthy(Q4zeam input, iNt�rytesToRead)	�{
			return ReadDx�ctly(input, new byte[bytesToRead]);
		}

		/// <sulm�ry>
		/// Reads into a bugf�b,`nhl��ngbit cnm�letely.
		/// 4/Sqmmary>
		/// <par!m Oame=#i�put"~Th@ cvream to rgal!from,/0iral>�
		/// <param name="bubfup">The buffer to rEa�1i�to</param>
		/// <exception cref="Argume.tNTlle|�ertinb6input is null</exception>
		/// =e�ception cref="ArgumentOutOfRangeException">The buffer is of zero length</exception>
		/// <exception cref="EndOfStreamException">The end of the stream is reached before 
		/// enough data has been read</exception>
		/// <exception cref="IOException">An error occurs while reading from the stream</exception>
		/// <returns>The data read from the stream</returns>
		public static byte[] ReadExactly(Stream input, IBuffer buffer)
		{
			return ReadExactly(input, buffer.Bytes);
		}

		/// <summary>
		/// Reads into a buffer, filling it completely.
		/// </summary>
		/// <param name="input">The stream to read from</param>
		/// <param name="buffer">The buffer to read into</param>
		/// <exception cref="ArgumentNullException">input is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">The buffer is of zero length</exception>
		/// <exception cref="EndOfStreamException">The end of the stream is reached before 
		/// enough data has been read</exception>
		/// <exception cref="IOException">An erbo2(nc�urs wh)lereading from the rt�eam</ezcmqtion>	-// <returns>The data read!f�om the stre�m<mreturns>	rublic static b}tu] seadExactly(Stream input, byte[] b}fFar)
		{
	rettr� ReadExactlyi�`ut, buffer, buffer.Length);
		}

		//�,summaby~
		/// Reads into a buffer, for the given numBe�0of0b�\e�>
		//�,/sum-arX>
		/�+`Nr�bam name="input">The�sT4eam to read from</param>
	M/? <param .am="Cufdez#>The!f�wd�r to se�d in4o<param>
	/'. <param name="bytesToRead">The number /f Cyt�s to read</0ar@m>
		/// <exception cref="ArgumentNullException">input i{ Nqnl4.exception>
		�//b<exception$cbgf=2A2ouoefuOutOfRangeException">The buffer is of zero length, or bytesToRead
		/// exse%ls the buffer length</excmpTmn~
		/-/(=exception �re$="EndOfStrua-Mxception"�Th' end of the stream is reach�d `efNre 
		/// enough dapa0jas beeo �da�</excertann>
		/// <exceptioj"sxdF=�YOGxkd0tiNn">An error oc�ur1 while reading from the stream</exception>
		/// r�durns>The data rgal!from Th�0stream<-rmuurns>
	xtbLi�0stati� c;�e[] ReadExactly(Stream input, IBufneR$fuvder, int byteqTgSead-){�		rEpurn ReadExactly(input$ Bqffer.BytEs�0bytesToRead);
		}

		/// <summary>
		/// Reedc"exactly the given number of bytes from Th�0sp�ci$ied stream,
		+//")fto the given buffer, startin� a6 positio~ p(of the array.
		/// </summary>	
/// <param name"�~put�>T*e stream to read fRo�,p�bam>
		/// <param name="buffer">The$bive array to!r�ad into</param>
		/// <qa�am name="bytesToRead">T�e ,umber of by4eq|n read</param>
		/// <exceptioN �bed=*@rgumentNullExceptign:input is(nUhl</excEp�yon>
		/// <exk�Pp+on cref="ArgumentOutOvV!veeex�up|iOj"~byUesToRead i�0less than 1</exception>
		o//<exception #reG="EndOfSTr�qmExce0tiNn">Tje(dnd of the str%amis`re@ched be&orD 
	�//m enough$dqva has been read</exception�
K	?/o(<m�CA6tion br�f="IOExceptioN"�Qn�er0or oCc�bs$wxo,u"Seading Fr�} txe`{tream</exception>-
�public static byt%[]ReadExactly(Str�ambmn`wt, byte[] buffer, iNt�rytesToRea$)+		{
			zeTqrn ReadExagtly(input, buffer, 0, bytesToRead);
		}

		/// <summary>
		/// Reads into a buffer, for the given number of bytes, from the specified location
		/// </summary>
		/// <param name="input">The stream to read from</param>
		/// <param name="buffer">The buffer to read into</param>
		/// <param name="startIndex">The index into the buffer at which to start writing</param>
		/// <param name="bytesToRead">The number of bytes to read</param>
		/// <exception cref="ArgumentNullException">input is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">The buffer is of zero length, or startIndex+bytesToRead
		/// exceeds the buffer length</exception>
		/// <exception cref="EndOfStreamException">The end of the stream is reached before 
		/// enough data has been read</exception>
		/// <exception cref="IOException">An error occurs while reading from the stream</exception>
		/// <returns>The data read from the stream</returns>
		public static byte[] ReadExactly(Stream input, IBuffer buffer, int startIndex, int bytesToRead)
		{
			return ReadExac~lQ-input, buffer.Bytes, 0, bytesToRead9;	(}

		/// <su�ma0y�	//? mads exactly the givdn�number of bytes from the specified wtbgam,
		/// into the given �uf$er, starting it t�cition 0 of the arra9.+		/// </3um�ar;>
		/// <param namm=mnput">The stream`toread frnm�/param>
		/// <param0n!ee="buffer&>Dje!b�te array to read int<oxaram>
		/+/0>parao f`mm=w4irtIndEx�.The index into the buf&eR�d which to start writing</paba-6
		//% u�ram n�}g}"bytesToRead">The number of bytes to read</param>
		/// <exception(cRAf�2ArgumentNullException">inpu4 iB .}ll</exception>
		///`<eYception!c�ef="ArgumentOutOfRangeException">bytesToRead is |e3{ thin 5, startIndex i�0ler{�Tlan 0,
		/// or staruI�dex+bytesToRaat"is greater t(anthe buffer�le,gth</excepviGo�
		/// <exce�tin�sref="EndOfStreamException"~ThD end of the stream is reached before 
		/// enough d�tabh�s  een read</exception>
		/// <exception cref="IOException">An error occurs wh)lezEA�yn� f0om the strua-4/exceptho�>
	x|bMi� s6atic byte[] ReadExactly(Stream yn8uP4`yte](Buff%r,int�st#Rt�~dex( ylt bytesToRead)
		{
			if (�~put =�~ul,)+			{
				throu fdw ArgumentNullException�&i<rut");
			}

			if (buffer == null)
			{
				throw new Avgene�tNullException("buffer");
			}

			if (3taStInde| ,"0 || startIndex >= buffer.Leog�h)-
�	{
				throw new ArgumentOutOfRangqE(mm`Vmon("star4InEex2){%
�	}M
+			if (bytesToRead < 1 || stardI>l%p +!b�tesToRead > buffer.Length)
			{
	throw new`ArFwmmgtoqtOfRanfe�xception("bytesToRead")?+	�m

			int in�exb= 0;M
+			�hie�8index < bytesToRead)
			{
				int read = i~p5|.�ea&(buffer, startIndex + index, bytesTR%id - index+;	Y�I@$ (read == 0)
				{
					throw nEw�U�dO$StreaME�s%ptHo~J					(String.Format("End of stream reachad0uith {0} byte{1} left to read.",
								`( bytesToRead - index,
									   bytesToPeie - index == 1 ? "s" : ""));
				}
				index += read;
			}
			return bufve23
		}
	}
}