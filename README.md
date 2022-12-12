# VineGeneration

Procedural Mesh Generation of a Vine

# Brief

Write a single Unity component that generates the growing spline in the scene view.
Imagine bean stalk or vine stem growing upwards. Starting with single point and growing up.

1. Age - slider in the inspector controlling the age of the stem. The older the stem the
longer it gets
2. Rate of Growth
3. Roughness. Stem to be divided into segments that have some angle deviation to
resemble the vine stem. Low roughness means smooth upward stem. High
roughness means lots of deviation between segments of the stem
4. Thickness, Thickness of the stem procedurally generated mesh. The cross section of
the stem can be perfect circle

## Approach

As this exercise is focused on predural mesh generation, I'll focus my efforts around that implementation. When thinking of a vine, it is based on a spline. I'll leverage the Spline Unity package to does this, and then implement custom mesh generate code to create a segmented cylinder around that line.


1. Spline Distrubtion

   1. Generate spline with control points (CP).

   2. CPs are placed along the y axis with some perpendiculuar random x/z offset.

   3. As each CPs is added the Vector is offset slgihtly using noise.


 - Age will control the length of the spline.
 - Roughness will control the number of control points on the spline with a random offset. Calculation to the number of control points will also be considered.
 - Thickness simply controls the radius of the cylinder, however each higher segment of the cylinder will scale down linearly like a cone.


2. Triangulation

   1. A 3D polygon can be generated along the spline, this will be based on a circle. These are called the stacks along the cylinder.

   2. The number of stacks along the spline defines the quality of vine with respect to length.

   3. The number of verties within each polygon define the subdivion quality with respect to the circumernce of the vine mesh, these are called the slices when thinking of a cylinder.

   4. Mesh triangulation is then applied by joining the verticies of two relative circle stacks, and around the slice, to defined a set of quad faces along the whole spline.
