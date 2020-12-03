Public Class Form1
    Dim xdir As Integer
    Dim ydir As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub move(p As PictureBox, x As Integer, y As Integer)
        p.Location = New Point(p.Location.X + x, p.Location.Y + y)

    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)

            Case Keys.Left
                MoveTo(PictureBox1, -5, 0)
                'moveto(PictureBox1, -5, 0
                xdir = -5
                ydir = 0

            Case Keys.Down
                MoveTo(PictureBox1, 0, 5)
            Case Keys.Up
                MoveTo(PictureBox1, 0, -5)
                'moveto(PictureBox1, 0, -5
                xdir = 0
                ydir = -5

            Case Keys.Right
                MoveTo(PictureBox1, 5, 0)

            Case Keys.Space
                PictureBox2wall.Visible = Not PictureBox2wall.Visible

            Case Keys.Space
                PictureBox4wall.Visible = Not PictureBox2wall.Visible

            Case Keys.Space
                PictureBox5wall.Visible = Not PictureBox2wall.Visible

            Case Keys.Space
                PictureBox6wall.Visible = Not PictureBox2wall.Visible

            Case Else

        End Select
    End Sub
    Sub Follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(PictureBox1.Location)
        headstart = headstart + 2
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Public Sub Chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > PictureBox1.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < PictureBox1.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub



    Function Collision(p As PictureBox, t As String)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If p.Bounds.IntersectsWith(obj.Bounds) And (obj.Name.ToUpper.EndsWith(t.ToUpper) Or obj.Name.ToUpper.StartsWith(t.ToUpper)) Then
                col = True
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t

    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If p.Name = "PictureBox1" And Collision(p, "WIN", other) Then
            Me.BackColor = Color.Green
            other.visible = False
            Return

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Chase(angrybenson)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
End Class