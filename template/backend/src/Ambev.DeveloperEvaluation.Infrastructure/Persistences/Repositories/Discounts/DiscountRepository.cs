using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Infrastructure.EF.Context;

namespace Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories.Discounts;

public class DiscountRepository(DefaultContext context) : BaseRepository<Discount>(context), IDiscountRepository;