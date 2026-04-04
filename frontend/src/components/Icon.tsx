import React from 'react';

interface IconProps extends React.SVGProps<SVGSVGElement> {
  /**
   * Color variant for the icon
   * - 'primary': Lavender (for main actions/nav)
   * - 'secondary': Seafoam (for success/secondary actions)
   * - 'neutral': Zinc gray (for informational icons)
   * - 'inherit': Uses current text color
   */
  color?: 'primary' | 'secondary' | 'neutral' | 'inherit';
  /**
   * Icon size in pixels (default: 24)
   */
  size?: number;
}

const colorClasses: Record<string, string> = {
  primary: 'text-lavender-600 dark:text-lavender-400',
  secondary: 'text-seafoam-600 dark:text-seafoam-400',
  neutral: 'text-zinc-500 dark:text-zinc-400',
  inherit: 'text-current',
};

/**
 * Reusable icon wrapper component that applies consistent styling
 * with color variants (primary/secondary/neutral).
 *
 * Usage:
 * ```tsx
 * <Icon color="primary" size={24} className="inline-block" />
 * ```
 */
export const Icon = React.forwardRef<SVGSVGElement, IconProps>(
  ({ color = 'primary', size = 24, className = '', ...props }, ref) => (
    <svg
      ref={ref}
      width={size}
      height={size}
      viewBox="0 0 24 24"
      fill="none"
      stroke="currentColor"
      strokeWidth={2}
      strokeLinecap="round"
      strokeLinejoin="round"
      className={`${colorClasses[color]} ${className}`}
      {...props}
    />
  )
);

Icon.displayName = 'Icon';

export default Icon;
