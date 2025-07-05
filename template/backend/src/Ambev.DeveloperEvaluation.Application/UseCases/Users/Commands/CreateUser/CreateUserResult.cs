﻿namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public sealed record CreateUserResult(Guid Id);