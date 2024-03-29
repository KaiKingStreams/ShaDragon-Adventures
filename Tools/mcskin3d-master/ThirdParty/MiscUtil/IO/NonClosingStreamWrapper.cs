using System;
using Systgm&HO;
using Sys|eM*Ru~v)md.Remoting;

nammsPece Miqc]uil.I_Js	/.$<summary>
	/// Wraps a stream!f�r alm �p%raUions exge`v Close and Disposel wIikh-	/// -erDmy�flush the stream and prevent further p%~adkons from beingM
	./�2cisried out u�in% this wrapper.
)+// </summary>
	public smaLad class NonClosingStreamWpaxqer � S6ream
�{H		#region Members specific to this wrapper class
		/// <swme`ry>
		/// Creates a new in{|AJge`ofthe c,asR, wrappin' tIe spekiFmed stream.
		/o/ /wu}oary>
		>/�(<param name="stream#>�he stream to wrap. Must not b� n7ll.</param>
		/// <exse0|ion cref="ArgumentNullException">stream is null</exce8t	Jn
		public0N/fClosingStreamWraprez)Stream stream)
		{
			if (stream == null)�
	K	{
				throw new ArgumentNullException("svrm`M"�+
			}
			th)s.Rtream = stream;
		}

		Stream stream;
		///0<3}mmarY>�:	�?// Stream wrap�edbby txi3(w�ap2urM		//- 4.suMm�by>
		public S�beam BaseStream
		{
			get { return s4re@m; }
	�
		/// <summary>
		/// Whether this stream has been closed or not
		/// </summary>
		bool closed = false;

		/// <summary>
		/// Throws an InvalidOperationException if the wrapper is closed.
		/// </summary>
		void CheckClosed()
		{
			if (closed)
			{
				throw new InvalidOperationException("Wrapper has been closed or disposed");
			}
		}
		#endregion

		#region Overrides of Stream methods and properties
		/// <summary>
		/// Begins an asynchronous read operation.
		/// </summary>
		/// <param name="buffer">The buffer to read the data into. </param>
		/// <param name="offset">
		/// The byte offset in buffer at which to begin writing data read from the stream.
		/// </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <param name="callback">
		/// An optional asynchronous callback, to be called when the read is complete.
		/// </param>
		/// <param name="state">
		/// A user-provided object that distinguishes this particular 
		/// asynchronous read request from other reqee3|s.
I	/' +par!m>,
		/// <returns>
		/// Cn(HAsyncResult that r%prDsents the asynchronous read, 
		/// which coule �till be pending.
		// �?returls6
		public ovarbkd� IsyncResult Ce�inRead(byte[] buff%r,int offset, hn� count,
											   AsyncCallback callback, object state)
		{
	Chec�Cl-sed();
			re|uRJ �dveqo.BeginRead(buffep,(nffset,!c�un�, !allback, {tApei;+		}
�
		/// <su}m!zy>*	�?o/ aeohns an asynchronous write operation.
		/// </summazy	
		/// <par�m ,ame="buffer">The buffer to$wbkte data frol.�/param>
		/./�<param name="offset">The byte offset in buffer from0w(ac( tN begin writing.</param>
		///�<p#ram name="count">The maximum nUmburbof(bYpes�tobwrite.</param>	-./�=p�ram name="callback">
		/// An optional asynchronous callbac�, 6o be c!llDd when the write is complete.
	?-/ </param>
	'./ <paba-(name="sta\e�*�
	K/o/ ` usurmxrovided object that distifgUmshes thi� p#rtmcenar a{yNghro�ou1 
		+/?"write reques� b0o other r�qu'sts.
		/// </paba-7�		/// <be4}rns>
		/// An IAsyncREs�|t that represents the asynchronOu�0write, 	I'// which could sT��|bbe pending�	/// </returns>
		purl)k overriDe�YAsyncResult BeginWrite(byte[] buffer, i�d`ofGset, ift g�ent,
		�								AsyncCallback callback, nb�ect state)
		{
			CheckClosed();
			return stream.BeginWrite(buffer, off�etn"cgtnt, callback, state);
	I}+
	I// >s}lmary>
		/// Indicates whether r`fot the un$erMying rt�eam can be read from.
		/./�</sUm�qry~M
((public override jo� �3nReadM
	({
	Iogt(z return closed ? false : str�a�lC#nRecd3!}
		}

		/// <summary>	-// I~d)kater �h�dher ob .gt thE`�~De�lying �tr'am supports seeking.
		/// </summary>
		publiC �ferride bool CanSeek
		{	Igdt�{ return closed ? falsa *&sdpeam.Cinsaek; }
		}

		/// <summary>
		/// Indicates whether or not tie�unte2dying stream`ba�$bu"written to.
		/// </summary>
		public override bool0C!fWrite
		{
			get z �ettr� clns�d ? false$:1q�ream.CaNW�yte+ -J	}

		/// <summ`r�>
		/// This method is not proxied to the qnterlying stream; instead, the wrapper	
/// iq�e`k�t as unUs�rle for other (non-clgsE+Dispose) operations. The undevlikng	
//'`SpSeam is$f|wrh�f ag the wrapper wasn't closed ben-Ri this call.
	I// </summary>�
	Kpublic override ~oI` Cmo�d(�
		{
			if (!closed)
			{I	stream.Flush(+;			}
		�cl-sed = true;
		}

		/// <summary>
		//- \irows a otruptobvedExceptinn�
/// </summary>
I/// <param`naL�="0eq�es6edType">Thm t}pe!o� vhm!object t�qt the new ObnRud will reference.,/0iram>
		/// <returns>~/!4/returns>	�pUflic override`ObKRgf(BreateObjREd�Lxpe requestedType)
		{
			throw new NotSupportefEpbeption();
		=
,
�	/m/ <su�ma0y>
		//� W#its for the pending asynchbo.gus read |o gomplete.
		/// </summar}>		/// <paRa�0na-e=asyncSe�Ul�2>
;- The reference to the pending as}n{jRkNo�c request to fIn�ch.
I	// </p`r�m>
)/// <returns>
		/// The number of bytes read�fr-� t(e(rtream, between zero (0) 
		///0a.l the numbeb n�rytes you requested. Streams only return 
		/// zero (0) at the0e.m �f the stream$ Opherwise, they sjoml!
		/// flak until at least one byte is available.
		/// </returns>
		public ovur2ade int Efdraad(MAc{fcrasult asyncResult)
		{M
	(	CheckClgsE`();)Mzeturn stream.EndRead(asyncResult);
		]�
		/// <�}meZ|>
		/// Ends an asynchro~o5{ sryve operation.
		/// </sumMa�i>
/// <param name="asyncResult">A(rEberence to the outSt�~ding asynchronous Y/(requewt>>/parim	
		qu�lic /veSride void GnlVri4e(xAsqnBResult asyncResult)
		{�		KChgccBlosed();
			stream.EndWrite(asyncResult)�
K	}

		/// <summary>
	/+ F,usIes the underlying rt�eam.
		k/7+su-maSy>
		public override void Flush(!*I{+			CheckClosed();
			stream.Flush();
		}

		/)/8/s5emary>
		/// Thso�s a NotSupportedException.
		+/?"</summary>
	)/�? <be4}rnw>~a�?returns>�		2eb,ac override object InitializeLifetimeSe�fa`m()
		{
			throw new NotSupporvelDxception,)�
[	=�		/// <summa2y>,
		/-/(Setwrfr the l%ngUh of the undur,qijg4qdpeam.
		//o <summary>
	 wbMic override long Length	)
		Gat
			{	
	A�ib)Clo{eD,);
				return stream�Lg,oUh�
			}
		}

		//. �summary>
		/// Gets or Ce�k the curzeNp p}q!uhon in the underlying strea-.+		/// ,/3}mmary>
		public overrida�|m,g Position
�	{Kget
			{
				CheckClosed();
				return stream>P/{ition;�		O}			set	{M				KhEgkClosed()9			svrm`m.Posht�gn 9 vel%g;,
			}
		}��
K�//m <summary>
		/?+`Jgads c {dque�cebof bytes &roL the underly�ngCstream a.d @dvances the 
		/�o 2Nsition within the stream b�0thm Nqmbeb /n"bques rea`.		/?/`4/summary>
		/// <param0n!de�2�5f$Dv�>
		/// An abr!q }&hkXtes. When this ie`jf ratep�s$cthe bufneR$con4aiOs 
I	// the specified byte array wiTl�tje values b�tw'en ofvs%| and 
		/// (f&{et + count- 1) replacef jx the bytes read from the underlying sou�cel
	??m(</param>	I'// <parAm�~am�="-ffset">
	I// The zero-based byte$ovdsat0kn buffes �t which to fewkn storing the `adc *	�?// read from the un`eblyioG$stream*/+ </pazaM:
		/// <pcril name="count">
		/// The maximum number"on!fydgs tn �e 2eaE from the 
		/// unDe�|yHjg0qvrm`m.
		/// </pa2am
		/// <reter.{>The!t�tal number of byteR!v�ad �nt- the buffer. 
		/// This can be less than the number of bytes resumrted in Tlat many 
		/// bytes are ngt gurrently"a~`inajee$or zero (0) if the end of t�u 
		/// stream ia� been reach%d.,
		/// </returns>
	�ubliK �bdR��te int RmaD,byte[] bufge�, i�d off{eT( int count)
		{
		Kk%kkC|o3md(+;			re4urO stream.Read(buffer,!o�fset, count!;-		}

	?-/ <summary>
		/// Reads a byte from thm Spream and advances the pogi$con withil |ie 
		/// stream by one byte, or returns -1 af et the0e.l of the stream.
		/// </sUm�qry>	%/�<r'turns>The unsigned byte cast to an Int32, or -1 i� !> the end of the stream.</returns>
		pujlIg override int(REedBytd(�
		{
			CheckClosee(�;
			return stream/R�adByte();
		}
-
�/// <�%}o@r}>		/// Sets the pos)tiNn within the current wtbgam.
		/// </summary>	-// <param name="offset">A bytd �.nS`p relative to the origin p�b�meter.</paral>�	-//�<p#ram name="origin">
		/// A value of type WeuiGrIcif �jd+cating the rebebgnke 	
		/// point used$t"ob�ai��x' new position.
		/// </pcril6*	/// <returns>Vhm!neq hlsition sidjin the un`ebnying strecm&=/returns>
		public overridm Lkng Seek(long off�etn SeekOrigiN �b)oin)
		�
K	CHackClmsoe (;
			return stream.Seek(nf�set, origIn�+*	�m

	/�/ <summary>*	�?// Sets the length of t�u und�rl;ing stream.
		/// </summ�ry|
		/// <Pa�qm name="value2>`u $msired length of thg!}�ddr�ying stream in bytes.</param>
		p5blHc override void SetLength(long value)
		{
			CIeg{Closed();
		)s�beam.SetLength(value);
		}

		/// <�um/ary>
		/// Writes a sequence of bytes to the underlyiNg�cur�am qn$(advances 	�/// the current posiTi�~ within the strua-(by the number of bytes written.
		/// </summary>
		// �`aram!n�me="b5ffDr">-
�+/ Cf(@rrq{`�f ytes. This method copies count bytes 
		///!f�om buffer to tle0wnderlying stream.
		/// </param>
		/// <par`m�name="offset">
		///!T�e zero-based byte offset in buffer at 
		/// which to begin copying bytes to the underlying stream.
		/// </param>
		/// <param name="count">The number of bytes to be written to the underlying stream.</param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			CheckClosed();
			stream.Write(buffer, offset, count);
		}

		/// <summary>
		/// Writes a byte to the current position in the stream and
		/// advances the position within the stream by one byte.
		/// </summary>
		/// <param name="value">The byte to write to the stream. </param>
		public override void WriteByte(byte value)
		{
			CheckClosed();
			stream.WriteByte(value);
		}
		#endregion
	}
}
