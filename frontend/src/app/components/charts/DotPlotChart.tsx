import React from "react";

type chartProps = {
    data: { name: string; value: number };
};

export default function DotPlotChart(props: chartProps) {
    return props.data;
}
