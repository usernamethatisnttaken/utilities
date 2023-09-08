using OpenTK.Graphics.OpenGL4;

namespace glApp {
    /// <summary>
    /// Manages the shaders for the miniapp
    /// </summary>
    public class Shader {
        private readonly int programHandle, vertexHandle, fragmentHandle;

        /// <param name="vertexPath">Path to the vertex shader</param>
        /// <param name="fragmentPath">Path to the fragment shader</param>
        public Shader(string vertexPath, string fragmentPath) {
            //Initialize program handle
            programHandle = GL.CreateProgram();

            //Do vertex initialization
            vertexHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexHandle, File.ReadAllText(vertexPath));
            GL.CompileShader(vertexHandle);
            GL.AttachShader(programHandle, vertexHandle);

            //Do fragment initialization
            fragmentHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentHandle, File.ReadAllText(fragmentPath));
            GL.CompileShader(fragmentHandle);
            GL.AttachShader(programHandle, fragmentHandle);

            //Link the program and check for errors
            GL.LinkProgram(programHandle);
            DoErrors();

            //Remove the original shaders and delete them
            GL.DetachShader(programHandle, vertexHandle);
            GL.DetachShader(programHandle, fragmentHandle);
            GL.DeleteShader(vertexHandle);
            GL.DeleteShader(fragmentHandle);
        }

        /// <summary>
        /// Errors thrown by the shader programs are pushed to the console through here
        /// </summary>
        private void DoErrors() {
            GL.GetShader(vertexHandle, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexHandle);
                Console.WriteLine(infoLog);
            }

            GL.GetShader(fragmentHandle, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentHandle);
                Console.WriteLine(infoLog);
            }

            GL.GetProgram(programHandle, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(programHandle);
                Console.WriteLine(infoLog);
            }
        }

        /// <summary>
        /// Use the shaders
        /// </summary>
        public void Use() {
            GL.UseProgram(programHandle);
        }

        /// <summary>
        /// Find the location of an attribute belonging to a shader
        /// </summary>
        /// <param name="attribName">The name of the attribute</param>
        /// <returns>The location of the attribute</returns>
        public int GetAttribLocation(string attribName) {
            return GL.GetAttribLocation(programHandle, attribName);
        }
        
        public void Dispose() {
            GL.DeleteProgram(programHandle);
        }
    }
}